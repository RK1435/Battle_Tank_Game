using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Chase : EnemyTankBaseState
{
    public override void EnterState(EnemyTankState enemyTankState)
    {
        
    }

    public override void UpdateState(EnemyTankState enemyTankState)
    {
        enemyTankState.agent.SetDestination(enemyTankState.player.position);
        CheckEnemyAttack(enemyTankState);
        CheckEnemyPatrol(enemyTankState);
    }

    private void CheckEnemyAttack(EnemyTankState enemyTankState)
    {
        if(enemyTankState.distToPlayer <= enemyTankState.attackRange)
        {
            enemyTankState.SwitchState(enemyTankState.attackState);
        }
    }

    private void CheckEnemyPatrol(EnemyTankState enemyTankState)
    {
        if(enemyTankState.distToPlayer > enemyTankState.chaseRange)
        {
            enemyTankState.SwitchState(enemyTankState.patrollingState);
        }
    }
}
