using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoBehaviour
{
    [SerializeField]
    private TankFactory tankFactory;

    private int enemyDeathCount = 0;

    public GameObject Player { get; private set; }
    public List<GameObject> Enemy { get; private set; }
    
    private TankSpawner tankSpawner;
    private void Start()
    {
        Enemy = new List<GameObject>();
        Player = tankSpawner.CreateTank();
        for(int i = 0; i < tankFactory.enemySpawner.Length; i++)
        {
            Enemy.Add(tankFactory.SpawnEnemy());
        }
    }

    public Vector3? GetJoyDirection() => UIService.Instance.GetJoyMoveDirection();
    public bool IsFirePressed() => UIService.Instance.IsFirePressed();

    public void ShellFired(Transform firepoint, float multipler, float damage) => ShellService.Instance.ShellFired(firepoint, multipler, damage);

    public void EnemyDeath() 
    {
        enemyDeathCount++;
        ServiceEvents.Instance.OnEnemyDeath?.Invoke(enemyDeathCount);
    }
}
