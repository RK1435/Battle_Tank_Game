using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Create Tank Type", fileName = "NewTankType")]
public class TankTypeScriptableObject : ScriptableObject
{
    [Header("Properties")]
    public TankType tankType;
    public float maxHealth;

    [Header("Movement & Rotation")]
    public float _movementSpeed;
    public float _rotationSpeed;

    [Header("Power")]
    public float damage;
    public float range;

    [Header("Drops")]
    public float points;

    [Header("Material")]
    public Material color;
}
