using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask tankMask;
    public ParticleSystem explosionParticles;
    public AudioSource explosionAudio;
    public float maxDamage = 100f;
    public float maxVelocity = 20f;
    public float explosionForce = 1000f;
    public float maxLifeTime = 2f;
    public float explosionRadius = 5f;
    public Rigidbody shellBody;
    public MeshRenderer shellMesh;
    public Collider shellCollider;
    public LayerMask playerTankMask;
    
    private bool exploded = false;
    private ShellService shellService;

    void Start()
    {
        shellService = ShellService.Instance;
        Destroy(gameObject, maxLifeTime);
    }

    public void SetVelocity(float factor) => shellBody.velocity = factor * maxVelocity * transform.forward;

    public void SetShellProperties(Transform exitPoint, float multiplier, float damage)
    {
        SetShellActive(true);
        transform.SetPositionAndRotation(exitPoint.position, exitPoint.rotation);
        SetVelocity(multiplier);
        maxDamage = damage;

        exploded = false;
        Invoke(nameof(Explode), maxLifeTime);
    }

    private void Explode()
    {
        if(exploded)
            return;
        exploded = true;
        SetShellActive(false);
        GiveDamage();
        SetOffExplosion();
    }

    private void SetShellActive(bool active)
    {
        shellMesh.enabled = active;
        shellCollider.enabled = active;
    }

    private void GiveDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            IDamagable damagable = colliders[i].GetComponent<IDamagable>();
            
            if (damagable != null)
            {
                damagable.TakeDamage(CalculateDamage(colliders[i].transform.position));

                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

                if (targetRigidbody != null)
                    continue;
                targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

                TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

                if (!targetHealth)
                    continue;

                float damage = CalculateDamage(targetRigidbody.position);

                targetHealth.TakeDamage(damage);
            }  
        }

    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = (targetPosition - transform.position).magnitude;

        float relativeDistance = (explosionRadius - explosionDistance) / explosionDistance;

        float damage = relativeDistance * maxDamage;

        damage = Mathf.Max(0f, damage);

        return damage;    
    }

    private void SetOffExplosion()
    {
        explosionParticles.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(-90, 0, 0));

        explosionParticles.transform.parent = null;
        explosionParticles.Play();
        explosionAudio.Play();

        float waitTime = Mathf.Max(explosionParticles.main.duration, explosionAudio.clip.length);
        StartCoroutine(FreeItemToPool(waitTime));
        Destroy(explosionParticles.gameObject, explosionParticles.main.duration);
        Destroy(gameObject);
    }

    IEnumerator FreeItemToPool(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        shellService.FreeShell(this);
    }

    private void Update()
    {
        transform.forward = shellBody.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }
}
