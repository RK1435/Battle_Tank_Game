using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Patrol : EnemyTankBaseState
{
    public override void EnterState(EnemyTankState enemyTankState)
    {
        EnemyPatrol(enemyTankState);
    }

    public override void UpdateState(EnemyTankState enemyTankState)
    {
        if(enemyTankState.agent.remainingDistance <= enemyTankState.agent.stoppingDistance)
        {
            enemyTankState.agent.SetDestination(enemyTankState.wayPoints[UnityEngine.Random.Range(0, enemyTankState.wayPoints.Count)].position);
        }

        if(enemyTankState.distToPlayer < enemyTankState.chaseRange)
        {
            enemyTankState.SwitchState(enemyTankState.chaseState);
        }
    }

    void EnemyPatrol(EnemyTankState enemyTankState)
    {
        GameObject[] wayPointsObject = GameObject.FindGameObjectsWithTag("WayPoint");
        Transform[] wayPointsObjectTransform = new Transform[wayPointsObject.Length];
        for(int i = 0; i < wayPointsObject.Length; i++)
        {
            wayPointsObjectTransform[i] = wayPointsObject[i].transform;
        }

        foreach(Transform wPOT in wayPointsObjectTransform)
        {
            enemyTankState.wayPoints.Add(wPOT);
            enemyTankState.agent.SetDestination(enemyTankState.wayPoints[UnityEngine.Random.Range(0, enemyTankState.wayPoints.Count)].position);
        }
    }
}
