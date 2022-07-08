//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyTankView : MonoBehaviour, IDamagable
{
    public TankType tankType;
    private EnemyTankController enemyTankController;
    private EnemyTankView enemyTankView;
    private EnemyTankModel enemyTankModel;
    private float enemyMovement;
    private float enemyRotate;
    private EnemyTankSpawner enemyTankSpawner;
    private float waitTime;

    [Header("Components")]
    public Rigidbody enemyRB;
    public MeshRenderer[] enemyMeshes;
   

    // Enemy Health
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
    public float enemyTankHealth;

    [Header("Firing")]
    public Transform firePoint;
    public GameObject shell;
    void Start()
    {
        
        //UI

        GameObject enemyHealthCanvas = GameObject.FindGameObjectWithTag("EnemyHealthCanvas");
        enemyHealthCanvas.transform.SetParent(transform);
        enemyHealthCanvas.transform.position = this.transform.position;

        GameObject enemyHealthSlider = GameObject.FindGameObjectWithTag("EnemyHealthSlider");
        enemyHealthSlider.transform.SetParent(enemyHealthCanvas.transform);
        enemyHealthSlider.transform.position = enemyHealthCanvas.transform.position;

        GameObject enemyHealthFill = GameObject.FindGameObjectWithTag("EnemyFillAreaFill");
        enemyHealthFill.transform.SetParent(enemyHealthSlider.transform);
        enemyHealthFill.transform.position = enemyHealthSlider.transform.position;

        //GameObject enemyFireTransform = GameObject.FindGameObjectWithTag("EnemyFireTransform");
        //enemyFireTransform.transform.SetParent(transform);
       
        // Enemy Health
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
        slider = GameObject.FindGameObjectWithTag("EnemyHealthSlider").GetComponent<Slider>();
        slider.value = currentHealth;
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

    public void FireFunction()
    {
        enemyTankController.FireShell();
    }
    public EnemyTankView(TankTypeScriptableObject tankTypeScriptableObject)
    {
        type = tankTypeScriptableObject.tankType;
        enemyTankHealth = tankTypeScriptableObject.maxHealth;
    }

    public Rigidbody GetEnemyRigidbody()
    {
        return enemyRB;
    }


    public void SetEnemyTankController(EnemyTankController _enemyTankController)
    {
        enemyTankController = _enemyTankController;
    }

    public void SetMaterial(Material enemyMaterial)
    {
        for(int i = 0; i< enemyMeshes.Length; i++)
        {
            enemyMeshes[i].material = enemyMaterial;
        }
    }

    public void SetEnemyTankSpawner(EnemyTankSpawner _enemyTankSpawner)
    {
        enemyTankSpawner = _enemyTankSpawner;
    }
  
}
