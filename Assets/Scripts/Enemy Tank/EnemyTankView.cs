//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyTankView : MonoBehaviour
{
    public TankType tankType;
    private EnemyTankController enemyTankController;
    private EnemyTankView enemyTankView;
    private EnemyTankModel enemyTankModel;
    private float enemyMovement;
    private float enemyRotate;
    private EnemyTankSpawner enemyTankSpawner;
    private float waitTime;

    public Rigidbody enemyRB;
    
    public Transform moveSpot;
    public float startWaitTime;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    
    // Start is called before the first frame update
    void Start()
    {

        //UI

        GameObject enemyHealthCanvas = GameObject.FindGameObjectWithTag("EnemyHealthCanvas");
        enemyHealthCanvas.transform.SetParent(transform);


        GameObject enemyHealthSlider = GameObject.FindGameObjectWithTag("EnemyHealthSlider");
        enemyHealthSlider.transform.SetParent(enemyHealthCanvas.transform);


        GameObject enemyHealthFill = GameObject.FindGameObjectWithTag("EnemyFillAreaFill");
        enemyHealthFill.transform.SetParent(enemyHealthSlider.transform);

        moveSpot = GameObject.FindGameObjectWithTag("EnemyMoveSpot").transform;
        //moveSpot.transform.SetParent(transform);
        //moveSpot = moveSpot.transform;

        //EnemyTankController enemyTankController = GetComponent<EnemyTankController>();
        
        waitTime = startWaitTime;
        moveSpot.position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));

        
    }

    // Update is called once per frame
    void Update()
    {
        //EnemyMovement();
        //EnemyPatrol();
    }

    /*public void EnemyPatrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, enemyTankController.speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }*/

    public Rigidbody GetEnemyRigidbody()
    {
        return enemyRB;
    }

    public void SetEnemyTankController(EnemyTankController _enemyTankController)
    {
        enemyTankController = _enemyTankController;
    }

    public void SetEnemyTankSpawner(EnemyTankSpawner _enemyTankSpawner)
    {
        enemyTankSpawner = _enemyTankSpawner;
    }
  
}
