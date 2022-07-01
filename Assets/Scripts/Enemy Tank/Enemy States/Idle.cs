using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : EnemyTankState
{
    public Idle(EnemyTankController enemyTank) : base(enemyTank)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }
  
    public override void Update()
    {
        if(IsPlayerInChaseRange())
        {
            MoveToChaseState();
            return;
        }

        if(IsEnemyAwayFromSpawn())
        {
            enemyTank.GetAgent().SetDestination(enemyTank.enemySpawnPoint);
        }
        else
        {
            if(Random.Range(0, 5000) < 10)
            {
                nextState = new Patrol(enemyTank);
                stage = EVENT.EXIT;
            }
        }   
    }

    private void MoveToChaseState()
    {
        nextState = new Chase(enemyTank);
        stage = EVENT.EXIT;
    }

    private bool IsPlayerInChaseRange()
    {
        if (playerTank == null)
            return false;

        float distance = Vector3.Distance(enemyTank.GetPosition(), playerTank.transform.position);

        if (distance < 15)
            return true;

        return false;
    }

    private bool IsEnemyAwayFromSpawn() => Vector3.Distance(enemyTank.GetPosition(), enemyTank.enemySpawnPoint) > 0.5;

    public override void Exit()
    {
        base.Exit();
    }

}
