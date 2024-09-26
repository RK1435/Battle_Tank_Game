using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int playerNumber = 1;
    public GameObject shell;
    public Transform fireTransform;
    public Slider aimSlider;
    public AudioSource shootingAudio;
    public AudioClip chargingClip;
    public AudioClip fireClip;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.075f;

    private Joybutton fixedJoybutton;
    //private string fire;
    private float currentLaunchForce;
    private float chargeSpeed;
    private bool fire;
    
    void OnEnable()
    {
        aimSlider = GameObject.FindGameObjectWithTag("PlayerAimSlider").GetComponent<Slider>();
        aimSlider.transform.SetParent(GameObject.FindGameObjectWithTag("Tank Health Canvas").transform);
        aimSlider.value = minLaunchForce;

        fireTransform = GameObject.FindGameObjectWithTag("PlayerFireTransform").transform;
        //shell = GameObject.Find("Shell").GetComponent<Rigidbody>();
        fixedJoybutton = FindObjectOfType<Joybutton>();
        //fixedJoybutton = Input.GetKey(KeyCode.Joystick1Button0);

        currentLaunchForce = minLaunchForce;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(shell, fireTransform.position, fireTransform.rotation);

        //fixedJoybutton = Input.GetKey(KeyCode.Joystick1Button0);

        
        //fixedJoybutton = "Fire" + playerNumber ;

        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        //bool keyDown = Input.GetButtonDown(fixedJoybutton);
        //bool key = Input.GetButton(fixedJoybutton);
        //bool keyUp = Input.GetButtonUp(fixedJoybutton);

        aimSlider.value = minLaunchForce;

        if (currentLaunchForce >= maxLaunchForce && !fixedJoybutton.Pressed)
        {
            currentLaunchForce = maxLaunchForce;
            Fire();
        }
        else if(fixedJoybutton.Pressed)
        {
            fire = false;
            currentLaunchForce = minLaunchForce;

            shootingAudio.clip = chargingClip;
            shootingAudio.Play();
        }
        else if(fixedJoybutton && !fire)
        {
            //fire = true;
            currentLaunchForce += chargeSpeed * Time.deltaTime;
            aimSlider.value = currentLaunchForce;
            
        }
        else if(!fixedJoybutton.Pressed && !fire)
        {
            Fire();   
        }
        
    }

    private void Fire()
    {
        fire = true;

        GameObject shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation);
        shellInstance.GetComponent<Rigidbody>().velocity = currentLaunchForce * fireTransform.forward;

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentLaunchForce = minLaunchForce;
    }

}
