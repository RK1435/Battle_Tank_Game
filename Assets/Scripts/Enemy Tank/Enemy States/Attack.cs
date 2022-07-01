using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : EnemyTankState
{
    private float fireRate = 1.5f;
    private float firePointHeight;
    private float firePointAngle;
    private float maxBulletVelocity;
    private float bulletVelocityFactor;
    private float timer;
    public Attack(EnemyTankController enemyTank) : base(enemyTank)
    {
        timer = 0;
        enemyTank.GetAgent().isStopped = true;
        firePointHeight = enemyTank.GetFirePoint().position.y;
        firePointAngle = enemyTank.GetFirePoint().position.x;
        maxBulletVelocity = 20f;
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

        if(GetPlayerDistance() > 15)
        {
            nextState = new Chase(enemyTank);
            stage = EVENT.EXIT;
        }

        else
        {
            Shooting(GetPlayerDistance());
        }
    }

    private float GetPlayerDistance()
    {
        return Vector3.Distance(enemyTank.GetPosition(), playerTank.position);
    }

    private void MoveToIdleState()
    {
        nextState = new Idle(enemyTank);
        stage = EVENT.EXIT;
        return;
    }

    private void Shooting(float distance)
    {
        timer += Time.deltaTime;
        enemyTank.GetAgent().transform.LookAt(playerTank.position);
        if(timer >= fireRate)
        {
            enemyTank.FireShell(CalculateVelocityFactor(distance));
            timer = 0;
        }
    }

    private float CalculateVelocityFactor(float distance)
    {
        float bulletVelocity = CalculateVelocity(distance);
        bulletVelocityFactor = bulletVelocity / maxBulletVelocity;
        Debug.Log("Fire Factor" + bulletVelocity);
        return bulletVelocityFactor;
    }

    private float CalculateVelocity(float distance)
    {
        return Mathf.Sqrt((float)(Mathf.Pow(distance, 2) / ((firePointHeight + Mathf.Tan(firePointAngle) * distance) * Mathf.Pow(Mathf.Cos(firePointAngle), 2))));
    }

    public override void Exit()
    {
        enemyTank.GetAgent().isStopped = false;
        base.Exit();
    }

}
