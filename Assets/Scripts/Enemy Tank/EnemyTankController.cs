using UnityEngine;
using UnityEngine.AI;
public class EnemyTankController
{
    private EnemyTankModel enemyTankModel;
    private EnemyTankView enemyTankView;
    private Rigidbody enemyRB;


    public delegate void EnemyKilled();
    public static event EnemyKilled onEnemyKilled;

    private float waitTime;
    


    public EnemyTankController(EnemyTankModel _enemyTankModel, EnemyTankView _enemyTankView)
    {
        enemyTankModel = _enemyTankModel;
        enemyTankView = GameObject.Instantiate<EnemyTankView>(_enemyTankView);
        enemyRB = enemyTankView.GetEnemyRigidbody();

        enemyTankModel.SetEnemyTankController(this);
        enemyTankView.SetEnemyTankController(this);
    }

    void Start()
    {
        setRigidbodyState(true);
        setColliderState(false);

        waitTime = enemyTankView.startWaitTime;
        enemyTankView.moveSpot.position = new Vector3(Random.Range(enemyTankView.minX, enemyTankView.maxX), 0, Random.Range(enemyTankView.minZ, enemyTankView.maxZ));
    }

    void Update()
    {
        EnemyPatrol();
    }

    public void EnemyPatrol()
    {
        enemyTankView.transform.position = Vector2.MoveTowards(enemyTankView.transform.position, enemyTankView.moveSpot.position, enemyTankModel.enemyMovementSpeed);

        if(Vector2.Distance(enemyTankView.transform.position, enemyTankView.moveSpot.position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                enemyTankView.moveSpot.position = new Vector3(Random.Range(enemyTankView.minX, enemyTankView.maxX), 0, Random.Range(enemyTankView.minZ, enemyTankView.maxZ));
                waitTime = enemyTankView.startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
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
