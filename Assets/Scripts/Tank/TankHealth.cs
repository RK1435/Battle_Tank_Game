using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float startingHealth = 100f;
    public Slider slider;
    public Image fillImage;
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;
    public GameObject explosionPrefab;

    private AudioSource explosionAudio;
    private ParticleSystem explosionParticles;
    private float currentHealth;
    private bool isPlayerDead;

    private TankView tankView;


    private void Start()
    {
        setHealthUI();
    }

    private void Awake()
    {
        explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionAudio = explosionParticles.GetComponent<AudioSource>();
        explosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        currentHealth = startingHealth;
        isPlayerDead = false;

        //setHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        setHealthUI();

        if(currentHealth <= 0f && !isPlayerDead)
        {
            onDeath();
        }
    }

    private void setHealthUI()
    {
        slider = Slider.FindObjectOfType<Slider>();
        slider.value = currentHealth;
        fillImage = Image.FindObjectOfType<Image>();
        fillImage.color = Color.Lerp(a: zeroHealthColor, b: fullHealthColor, t: currentHealth / startingHealth);
    }

    private void onDeath()
    {
        isPlayerDead = true;

        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive(true);

        explosionParticles.Play();
        explosionAudio.Play();

        gameObject.SetActive(false);
    }

    public void SetTankView(TankView _tankView)
    {
        tankView = _tankView;
    }
}
