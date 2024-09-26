//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TankView : MonoBehaviour
{
    public TankType tankType;
    private TankController tankController;
    private TankHealth tankHealth;
    private TankView tankView;
    private float movement;
    private float rotation;
    
    public Rigidbody rb;

    //Joystick
    private Joystick tvJoystick;

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

    void OnEnable()
    {
        aimSlider = GameObject.FindGameObjectWithTag("PlayerAimSlider").GetComponent<Slider>();
        aimSlider.transform.SetParent(GameObject.FindGameObjectWithTag("Tank Health Canvas").transform);
        aimSlider.value = minLaunchForce;

        fireTransform = GameObject.FindGameObjectWithTag("PlayerFireTransform").transform;
        //shell = GameObject.Find("Shell").GetComponent<Rigidbody>();
        fixedJoybutton = GameObject.FindGameObjectWithTag("FixedJoybutton").GetComponent<Joybutton>();
        //fixedJoybutton = Input.GetKey(KeyCode.Joystick1Button0);

        currentLaunchForce = minLaunchForce;
    }

    // Start is called before the first frame update
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

    }

    // Update is called once per frame
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


}
