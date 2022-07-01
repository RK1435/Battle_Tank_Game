using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankState
{
    public enum STATE
    {
        IDLE,
        PATROL,
        CHASE,
        ATTACK
    }

    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    }

    protected STATE name;
    protected EVENT stage;

    protected EnemyTankState nextState;
    protected EnemyTankController enemyTank;
    protected Transform playerTank;

    public TankView tankView;
    public EnemyTankState(EnemyTankController enemyTank)
    {
        this.enemyTank = enemyTank;
        stage = EVENT.ENTER;
        if(tankView != null)
        {
            playerTank = tankView.transform;
        }
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public EnemyTankState Process()
    {
        if(stage == EVENT.ENTER) Enter();
        if(stage == EVENT.UPDATE) Update();
        if(stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}
