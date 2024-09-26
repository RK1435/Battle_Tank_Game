using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Scriptable Object List", fileName = "NewTankListSO")]
public class TankScriptableObjectList : ScriptableObject
{
    public TankTypeScriptableObject[] TankList;
}
