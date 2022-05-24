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

        GameObject moveSpot = GameObject.FindGameObjectWithTag("EnemyMoveSpot");
        //moveSpot.transform.SetParent(transform);
        //moveSpot = moveSpot.transform;


    }

    // Update is called once per frame
    void Update()
    {
    
             
    }

    /*public void EnemyMovement()
    {
        enemyMovement = enemyTankController.EnemyPatrol();
    }*/

    public Rigidbody GetEnemyRigidbody()
    {
        return enemyRB;
    }

    public void SetEnemyTankController(EnemyTankController _enemyTankController)
    {
        enemyTankController = _enemyTankController;
    }

  
}
