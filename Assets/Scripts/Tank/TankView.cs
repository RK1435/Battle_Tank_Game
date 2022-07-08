using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TankView : MonoBehaviour, IDamagable
{
    public TankType tankType;
    private TankController tankController;
    private TankHealth tankHealth;
    private TankView tankView;
    private TankSpawner tankSpawner;
    private float movement;
    private float rotation;
    
    public Rigidbody rb;

    //Joystick
    public Joystick tvJoystick;

    //Audio
    public AudioClip engineIdling;
    public AudioClip engineDriving;
    public AudioSource movementAudio;
    private float originalPitch;
    public float pitchRange = 0.2f;

    //Shooting
    public int playerNumber = 1;
    public GameObject shell;
    public Transform fireTransform;
    public Slider aimSlider;
    public AudioSource shootingAudio;
    public AudioClip chargingClip;
    public AudioClip fireClip;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.75f;

    public Joybutton fixedJoybutton;

    //private string fire;
    public float currentLaunchForce;
    public float chargeSpeed;
    public bool fire;

    //Health

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

    void OnEnable()
    {
        aimSlider = GameObject.FindGameObjectWithTag("PlayerAimSlider").GetComponent<Slider>();
        aimSlider.transform.SetParent(GameObject.FindGameObjectWithTag("Tank Health Canvas").transform);
        aimSlider.value = minLaunchForce;

        fireTransform = GameObject.FindGameObjectWithTag("PlayerFireTransform").transform;
        fireTransform.transform.position = new Vector3(0, 1.7f, 1.35f);
        
        //shell = GameObject.Find("Shell").GetComponent<Rigidbody>();
        fixedJoybutton = GameObject.FindGameObjectWithTag("FixedJoybutton").GetComponent<Joybutton>();
        //fixedJoybutton = Input.GetKey(KeyCode.Joystick1Button0);

        currentLaunchForce = minLaunchForce;

        //Health
        currentHealth = startingHealth;
        isPlayerDead = false;

        //setHealthUI();

    }

    void Start()
    {
        GameObject cam = GameObject.Find("CameraRig");
        cam.transform.SetParent(transform);
        cam.transform.position = new Vector3(0f, 3f, -4f);

        //Joystick
        tvJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();

        //UI
        
        GameObject healthCanvas = GameObject.FindGameObjectWithTag("Tank Health Canvas");
        healthCanvas.transform.SetParent(transform);

        GameObject healthSlider = GameObject.FindGameObjectWithTag("HealthSlider");
        healthSlider.transform.SetParent(healthCanvas.transform);
        
        GameObject healthFill = GameObject.FindGameObjectWithTag("FillAreaFill");
        healthFill.transform.SetParent(healthSlider.transform);

        GameObject fireTransform = GameObject.FindGameObjectWithTag("PlayerFireTransform");
        fireTransform.transform.SetParent(transform);

        //Audio
        originalPitch = movementAudio.pitch;

        //Shooting
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;

        //Health
        setHealthUI();
    }

    private void Awake()
    {
        explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionAudio = explosionParticles.GetComponent<AudioSource>();
        explosionParticles.gameObject.SetActive(false);
    }

    void Update()
    {
        Movement();
        EngineAudio();
        tankController.Shooting();
    }

   
    private void FixedUpdate()
    { 

        if (movement != 0)
        {
            tankController.Move(movement, tankController.GetTankModel().movementSpeed);
        }

        if (rotation != 0)
        {
            tankController.Rotate(rotation, tankController.GetTankModel().rotationSpeed);
        }
    }

    private void Movement()
    {
        //Player Movement
        movement = tvJoystick.Vertical;
        rotation = tvJoystick.Horizontal;
    }

    private void EngineAudio()
    {

        if (Mathf.Abs(movement) < 0.1f && Mathf.Abs(rotation) < 0.1f)
        {
            if (movementAudio.clip == engineDriving)
            {
                movementAudio.clip = engineIdling;
                movementAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudio.Play();
            }
        }

        else
        {
            if (movementAudio.clip == engineIdling)
            {
                movementAudio.clip = engineDriving;
                movementAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudio.Play();
            }
        }

    }

    public void Fire()
    {
        fire = true;

        GameObject shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation);
        shellInstance.GetComponent<Rigidbody>().velocity = currentLaunchForce * fireTransform.forward;

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentLaunchForce = minLaunchForce;
    }

    //Health
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
        slider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        slider.value = currentHealth;
        //fillImage = Image.FindObjectOfType<Image>();
        fillImage = GameObject.FindGameObjectWithTag("FillAreaFill").GetComponent<Image>();
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


    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }

    public void SetTankHealth(TankHealth _tankHealth)
    {
        tankHealth = _tankHealth;
    }

    public void GetPlayerMovement() => Movement();
}
