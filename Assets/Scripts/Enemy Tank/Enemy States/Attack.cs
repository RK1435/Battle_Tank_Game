using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Attack : EnemyTankBaseState
{
    public override void EnterState(EnemyTankState enemyTankState)
    {
        enemyTankState.agent.SetDestination(enemyTankState.agent.transform.position);
    }

    public override void UpdateState(EnemyTankState enemyTankState)
    {
        AttackFunction(enemyTankState);
        enemyTankState.agent.transform.LookAt(enemyTankState.player.transform);
        if(enemyTankState.distToPlayer > enemyTankState.attackRange && enemyTankState.attackRange < enemyTankState.chaseRange)
        {
            enemyTankState.SwitchState(enemyTankState.chaseState);
        }
    }

    private void AttackFunction(EnemyTankState enemyTankState)
    {
        if(!enemyTankState.isAlreadyAttacked)
        {
            enemyTankState.enemyTankView.FireFunction();
            enemyTankState.isAlreadyAttacked = true;
            enemyTankState.StartCoroutine(ResetAttack(enemyTankState));
        }
    }

    private IEnumerator ResetAttack(EnemyTankState enemyTankState)
    {
        yield return new WaitForSecondsRealtime(enemyTankState.timeBtwAttack);
        enemyTankState.isAlreadyAttacked = false;
    }
}
