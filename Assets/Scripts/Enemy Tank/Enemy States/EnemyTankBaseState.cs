using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyTankBaseState
{
    public abstract void EnterState(EnemyTankState enemyTankState);
    public abstract void UpdateState(EnemyTankState enemyTankState);
}
