using UnityEngine;
using UnityEngine.UI;
public class TankModel
{
    private TankController tankController;

    public float movementSpeed;
    public float rotationSpeed;
    public TankType type;

    public TankModel(TankTypeScriptableObject tankTypeScriptableObject)
    {
        type = tankTypeScriptableObject.tankType;
        movementSpeed = tankTypeScriptableObject._movementSpeed;
        rotationSpeed = tankTypeScriptableObject._rotationSpeed;
    }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }

    public TankType tankType
    {
        get;
    }
}
