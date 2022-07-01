using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : EnemyTankState
{
    private Vector3 patrolPoint1;
    private Vector3 patrolPoint2;
    private Vector3 currentPoint;
    public Patrol(EnemyTankController enemyTank) : base(enemyTank)
    {
        name = STATE.PATROL;
    }

    public override void Enter()
    {
        patrolPoint1 = enemyTank.enemySpawnPoint + enemyTank.GetAgent().transform.forward * 5;
        patrolPoint2 = enemyTank.enemySpawnPoint - enemyTank.GetAgent().transform.forward * 5;
        currentPoint = patrolPoint1;
        base.Enter();
    }

    public override void Update()
    {
        if(playerTank != null)
        {
            if(IsPlayerInChaseRange())
            {
                MoveToChaseState();
                return;
            }
        }

        Patrolling();
    }

    private void MoveToChaseState()
    {
        nextState = new Chase(enemyTank);
        stage = EVENT.EXIT;
    }

    private bool IsPlayerInChaseRange()
    {
        float distance = Vector3.Distance(enemyTank.GetPosition(), playerTank.transform.position);
        if(distance < 15)
            return true;
        return false;
    }

    private void Patrolling()
    {
        enemyTank.GetAgent().SetDestination(currentPoint);
        if(Vector3.Distance(enemyTank.GetPosition(), patrolPoint1) < 0.5)
            currentPoint = patrolPoint2;
        if (Vector3.Distance(enemyTank.GetPosition(), patrolPoint2) < 0.5)
            currentPoint = patrolPoint1;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
