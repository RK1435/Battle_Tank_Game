using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyTankState : MonoBehaviour
{
    public EnemyTankBaseState currentState;
    public Patrol patrollingState = new Patrol();
    public Chase chaseState = new Chase();
    public Attack attackState = new Attack();

    internal EnemyTankView enemyTankView;

    public NavMeshAgent agent;
    public List<Transform> wayPoints = new List<Transform> ();
    internal Transform player;

    public float distToPlayer;
    public float chaseRange;
    public float attackRange;

    public float timeBtwAttack = 2f;
    public bool isAlreadyAttacked = false;

    private void Start()
    {
        enemyTankView = GetComponent<EnemyTankView>();
        player = FindObjectOfType<TankView>().transform;

        currentState = patrollingState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        CheckPlayer();
        currentState.UpdateState(this);
    }

    private void CheckPlayer()
    {
        if(player == null)
        {
            currentState = patrollingState;
            return;
        }

        distToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    public void SwitchState(EnemyTankBaseState enemyTankBaseState)
    {
        currentState = enemyTankBaseState;
        enemyTankBaseState.EnterState(this);
    }
}
