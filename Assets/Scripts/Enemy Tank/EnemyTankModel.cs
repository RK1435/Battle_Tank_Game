using UnityEngine;

public class EnemyTankModel
{
    private EnemyTankController enemyTankController;
    private EnemyTankView enemyTankView;
    public float enemyMovementSpeed;
    public float enemyRotationSpeed;
    public TankTypeScriptableObject type;
    public float currentHealth;
    
    public EnemyTankModel(TankTypeScriptableObject tankTypeScriptableObject)
    {
        type = tankTypeScriptableObject;
        enemyMovementSpeed = tankTypeScriptableObject._movementSpeed;
        enemyRotationSpeed = tankTypeScriptableObject._rotationSpeed;
        currentHealth = type.maxHealth;
    }

    public float GetSpeed() => type._movementSpeed;
    public Material GetColor() => type.color;
    public float GetHealth() => type.maxHealth;
    public float GetRotationSpeed() => type._rotationSpeed;
    public float GetDamage() => type.damage;

    public void SetEnemyTankController(EnemyTankController _enemyTankController)
    {
        enemyTankController = _enemyTankController;
    }

    public TankType tankType
    {
        get;
    }
}
