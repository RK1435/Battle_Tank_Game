using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTankHealth : MonoBehaviour
{
    public float startingHealth = 35f;
    public Slider slider;
    public Image fillImage;
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;
    public GameObject explosionPrefab;
    public TankType type;

    
    private AudioSource explosionAudio;
    private ParticleSystem explosionParticles;
    private float currentHealth;
    private bool isPlayerDead;
    private TankView tankView;
    private TankTypeScriptableObject tankTypeScriptableObject;
    private float enemyTankHealth;
    private EnemyTankSpawner enemyTankSpawner;

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
        enemyTankHealth = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        setHealthUI();

        if (currentHealth <= 0f && !isPlayerDead)
        {
            onDeath();
        }
    }

    private void setHealthUI()
    {
        //slider = Slider.FindObjectOfType<Slider>();
        slider = GameObject.FindGameObjectWithTag("EnemyHealthSlider").GetComponent<Slider>();
        slider.value = currentHealth;
        //fillImage = Image.FindObjectOfType<Image>();
        fillImage = GameObject.FindGameObjectWithTag("EnemyFillAreaFill").GetComponent<Image>();
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

    public EnemyTankHealth(TankTypeScriptableObject tankTypeScriptableObject)
    {
        type = tankTypeScriptableObject.tankType;
        enemyTankHealth = tankTypeScriptableObject.health;
       
    }

    public void SetEnemyTankSpawner(EnemyTankSpawner _enemyTankSpawner)
    {
        enemyTankSpawner = _enemyTankSpawner;
    }

}
