using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    public TankView tankView;
    private TankController tankController;
    private float movement;
    private float rotation;
    public TankScriptableObjectList tankList;
    private Vector3 playerSpawnPoint;

    void Start()
    {
        this.transform.position = tankView.transform.position;
        CreateTank();
    }

    private void CreateTank()
    {      
        TankTypeScriptableObject tankTypeScriptableObject = tankList.TankList[1];
        TankModel tankModel = new TankModel(tankTypeScriptableObject);
        TankController tankController = new TankController(tankModel, tankView, playerSpawnPoint);
    }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }
}
