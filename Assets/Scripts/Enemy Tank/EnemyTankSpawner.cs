using UnityEngine;

public class EnemyTankSpawner : MonoBehaviour
{
    public EnemyTankView enemyTankView;
    private EnemyTankHealth enemyTankHealth;
    private EnemyTankController enemyTankController;

    //public Transform[] SpawnPoints;
    //public GameObject EnemyPrefab;
    public int numOfEnemies;
    public TankScriptableObjectList tankList;
    //private Vector3 enemySpawnPoint;

    void Start()
    {
        StartGame();
    }

    private void OnEnable()
    {
        EnemyTankController.onEnemyKilled += CreateEnemyTank;
  
    }

    private void StartGame()
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            CreateEnemyTank();
        }
    }

    public Vector3 EnemyRandomPos()
    {
        float x, y, z;
        Vector3 pos;
        x = Random.Range(-35, 35);
        y = 1;
        z = Random.Range(-20, 30);
        pos = new Vector3(x, y, z);
        return pos;
    }

    public void CreateEnemyTank()
    {
        int index = Random.Range(0, tankList.TankList.Length);
        TankTypeScriptableObject tankTypeScriptableObject = tankList.TankList[index];
        EnemyTankModel enemyTankModel = new EnemyTankModel(tankTypeScriptableObject);
        EnemyTankController enemyTankController = new EnemyTankController(enemyTankView, enemyTankModel, EnemyRandomPos());   
    }

    public void SetEnemyTankHealth(EnemyTankHealth _enemyTankHealth)
    {
        enemyTankHealth = _enemyTankHealth;
    }

    public void SetEnemyTankController(EnemyTankController _enemyTankController)
    {
        enemyTankController = _enemyTankController;
    }
}
