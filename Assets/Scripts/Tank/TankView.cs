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
        

        //Audio
        originalPitch = movementAudio.pitch;    
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        EngineAudio();
    }

    private void Movement()
    {   
        //Player Movement
        movement = tvJoystick.Vertical;
        rotation = tvJoystick.Horizontal;

    }

    private void EngineAudio()
    {

        if (Mathf.Abs(movement) < 0.1f  && Mathf.Abs(rotation) < 0.1f)
        {
            if(movementAudio.clip == engineDriving)
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
