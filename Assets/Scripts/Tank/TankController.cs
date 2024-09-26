using UnityEngine;

public class TankController
{
    private TankModel tankModel;
    private TankView tankView;
    private Rigidbody rb;
   
    public TankController(TankModel _tankModel, TankView _tankView)
    {
        tankModel = _tankModel;
        tankView = GameObject.Instantiate<TankView>(_tankView);
        rb = tankView.GetRigidbody();

        tankModel.SetTankController(this);
        tankView.SetTankController(this);
    }

    public void Move(float movement, float movementSpeed)
    {
        rb.velocity = tankView.transform.forward * movement * movementSpeed;
    }

    public void Rotate(float rotate, float rotateSpeed)
    {
        Vector3 vector = new Vector3(0f, rotate * rotateSpeed, 0f);
        Quaternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    public void Shooting()
    {
        tankView.aimSlider.value = tankView.minLaunchForce;
        
        if (tankView.currentLaunchForce >= tankView.maxLaunchForce && !tankView.fire)
        {
            tankView.currentLaunchForce = tankView.maxLaunchForce;
            tankView.Fire();
        }
        else if (tankView.fixedJoybutton.Pressed)
        {
            tankView.fire = false;
            tankView.currentLaunchForce = tankView.minLaunchForce;

            tankView.shootingAudio.clip = tankView.chargingClip;
            tankView.shootingAudio.Play();
        }
        else if (tankView.fixedJoybutton && !tankView.fire)
        {
            
            tankView.currentLaunchForce += tankView.chargeSpeed * Time.deltaTime;
            tankView.aimSlider.value = tankView.currentLaunchForce;

        }
        else if (!tankView.fixedJoybutton.Pressed && !tankView.fire)
        {
            tankView.Fire();
        }
    }

    public TankModel GetTankModel()
    {
        return tankModel;
    }


}
