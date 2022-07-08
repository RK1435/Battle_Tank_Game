using UnityEngine;
using UnityEngine.AI;
public class EnemyTankController
{
    private EnemyTankModel enemyTankModel;
    private EnemyTankView enemyTankView;
    private Rigidbody enemyRB;
    public float speed;
    public TankController tankController;
    public TankView tankView;
    public delegate void EnemyKilled();
    public static event EnemyKilled onEnemyKilled;

    // Enemy Patrol 
    private EnemyTankState currentState;
    public EnemyTankSpawner enemyTankSpawner;

    //Enemy Shooting
    public int playerNumber = 1;
    
    public Transform fireTransform;
    
    public AudioSource shootingAudio;
    public AudioClip chargingClip;
    public AudioClip fireClip;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.75f;
    public float currentLaunchForce;
    public float chargeSpeed;
    public bool fire;

    public EnemyTankController(EnemyTankView _enemyTankView, EnemyTankModel _enemyTankModel, Vector3 enemyTankSpawnPoint)
    {
        enemyTankModel = _enemyTankModel;
        enemyTankView = GameObject.Instantiate<EnemyTankView>(_enemyTankView);

        enemyRB = enemyTankView.GetEnemyRigidbody();
        speed = enemyTankModel.enemyMovementSpeed;

        enemyTankModel.SetEnemyTankController(this);
        enemyTankView.SetEnemyTankController(this);

       
       // enemyTankSpawnPoint = enemyTankSpawner.EnemyRandomPos();
    }

    public NavMeshAgent GetAgent() => enemyTankView.GetComponent<NavMeshAgent>();
   

    public Vector3 GetPosition() => enemyTankView.transform.position;
    public Transform GetFirePoint()
    {
        return enemyTankView.firePoint;
    }

    void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);
    
    }
  
    public void Move(float enemyMovement, float enemyMovementSpeed)
    {
        enemyRB.velocity = enemyTankView.transform.forward * enemyMovement * speed;
    }

    public void Rotate(float enemyRotate, float enemyRotateSpeed)
    {
        Vector3 vector = new Vector3(0f, enemyRotate * enemyTankModel.enemyRotationSpeed, 0f);
        Quaternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
        enemyRB.MoveRotation(enemyRB.rotation * deltaRotation);
    }

    public void die()
    {
        setRigidbodyState(false);
        setColliderState(true);

        if(enemyTankView.gameObject != null)
        {
            EnemyTankView.Destroy(enemyTankView.gameObject, 3f);
        }

        if(onEnemyKilled != null)
        {
            onEnemyKilled();
        }

    }

    public virtual void FireShell()
    {
        //Mathf.Clamp(velocityMultiplier, 0.5f, 1f);
        //ShellService.Instance.ShellFired(enemyTankView.firePoint, velocityMultiplier, enemyTankModel.GetDamage());
        fire = true;

        GameObject shellInstance = GameObject.Instantiate(enemyTankView.shell, enemyTankView.firePoint.position, enemyTankView.firePoint.rotation);
        shellInstance.GetComponent<Rigidbody>().velocity = currentLaunchForce * enemyTankView.firePoint.forward;

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentLaunchForce = minLaunchForce;
    }

    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = enemyTankView.enemyRB.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        enemyTankView.GetComponent<Rigidbody>().isKinematic = !state;
    }

    void setColliderState(bool state)
    {
        Collider[] colliders = enemyTankView.enemyRB.GetComponentsInChildren<Collider>();

        foreach(Collider collider in colliders)
        {
            collider.enabled = state;
        }

        enemyTankView.GetComponent<Collider>().enabled = !state;
    }

    public EnemyTankModel GetEnemyTankModel()
    {
        return enemyTankModel;
    }

    public GameObject GetGameObject() => enemyTankView.gameObject;
}
