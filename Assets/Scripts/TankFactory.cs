using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFactory : MonoBehaviour
{
    [Header("Player Tank")]
    public TankView playerTankPrefab;
    public Transform playerSpawner;

    [Header("Enemy Tank")]
    public EnemyTankView enemyTankPrefab;
    public Transform[] enemySpawner;
    private int currentSpawn = 0;

    public List<TankTypeScriptableObject> tankTypes;
    public TankModel tankModel;
    public EnemyTankModel enemyTankModel;
    public EnemyTankSpawner enemyTankSpawner;

    public GameObject SpawnEnemy()
    {
        enemyTankSpawner.CreateEnemyTank();
        EnemyTankController enemyTank = new EnemyTankController(enemyTankPrefab, enemyTankModel, enemySpawner[currentSpawn].position);
        currentSpawn = (currentSpawn + 1) % enemySpawner.Length;
        return enemyTank.GetGameObject();
    }
}
