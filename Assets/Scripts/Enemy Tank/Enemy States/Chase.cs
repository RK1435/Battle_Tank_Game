using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : EnemyTankState
{
    public Chase(EnemyTankController enemyTank) : base(enemyTank)
    {
        name = STATE.CHASE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if(playerTank == null)
        {
            MoveToIdleState();
            return;
        }
        enemyTank.GetAgent().SetDestination(playerTank.transform.position);

        float distance = Vector3.Distance(enemyTank.GetPosition(), playerTank.transform.position);

        if(distance > 20)
        {
            MoveToIdleState();
        }
        else if(distance < 10)
        {
            nextState = new Attack(enemyTank);
            stage = EVENT.EXIT;
        }
    }

    private void MoveToIdleState()
    {
        nextState = new Idle(enemyTank);
        stage = EVENT.EXIT;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
