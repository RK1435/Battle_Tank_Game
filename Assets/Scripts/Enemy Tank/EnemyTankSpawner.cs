using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankSpawner : MonoBehaviour
{
    public EnemyTankView enemyTankView;
    private EnemyTankHealth enemyTankHealth;
    private EnemyTankController enemyTankController;

    public Transform[] SpawnPoints;
    public GameObject EnemyPrefab;

    public TankScriptableObjectList tankList;
    private Vector3 enemySpawnPoint;

    public void SetEnemyTankController(EnemyTankController _enemyTankController)
    {
        enemyTankController = _enemyTankController;
    }

    void Start()
    {
        CreateEnemyTank();
    }

    private void OnEnable()
    {
        EnemyTankController.onEnemyKilled += CreateEnemyTank;
    }

    private void CreateEnemyTank()
    {
        TankTypeScriptableObject tankTypeScriptableObject = tankList.TankList[0];
        EnemyTankModel enemyTankModel = new EnemyTankModel(tankTypeScriptableObject);
        EnemyTankController enemyTankController = new EnemyTankController(enemyTankModel, enemyTankView, enemySpawnPoint);
        enemySpawnPoint = enemyTankView.transform.position;
        //Instantiate(EnemyPrefab, SpawnPoints[1].transform.position, Quaternion.identity);
       // for (int i = 0; i < 4; i++)
        //{
          //  Instantiate(EnemyPrefab, SpawnPoints[i].transform.position, Quaternion.identity);

        //}
        
    }

    public void SetEnemyTankHealth(EnemyTankHealth _enemyTankHealth)
    {
        enemyTankHealth = _enemyTankHealth;
    }

}
