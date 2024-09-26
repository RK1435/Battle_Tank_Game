using UnityEngine;

public class EnemyTankModel
{
    private EnemyTankController enemyTankController;

    public float enemyMovementSpeed;
    public float enemyRotationSpeed;
    public TankType type;

    public EnemyTankModel(TankTypeScriptableObject tankTypeScriptableObject)
    {
        type = tankTypeScriptableObject.tankType;
        enemyMovementSpeed = tankTypeScriptableObject._movementSpeed;
        enemyRotationSpeed = tankTypeScriptableObject._rotationSpeed;
    }

    public void SetEnemyTankController(EnemyTankController _enemyTankController)
    {
        enemyTankController = _enemyTankController;
    }

    public TankType tankType
    {
        get;
    }
}
