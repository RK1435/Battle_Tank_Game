using UnityEngine;
using UnityEngine.AI;
public class EnemyTankController
{
    private EnemyTankModel enemyTankModel;
    private EnemyTankView enemyTankView;
    private Rigidbody enemyRB;
    public float speed;

    public delegate void EnemyKilled();
    public static event EnemyKilled onEnemyKilled;

    public EnemyTankController(EnemyTankModel _enemyTankModel, EnemyTankView _enemyTankView)
    {
        enemyTankModel = _enemyTankModel;
        enemyTankView = GameObject.Instantiate<EnemyTankView>(_enemyTankView);


        enemyRB = enemyTankView.GetEnemyRigidbody();
        speed = enemyTankModel.enemyMovementSpeed;
        enemyTankModel.SetEnemyTankController(this);
        enemyTankView.SetEnemyTankController(this);
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


}
