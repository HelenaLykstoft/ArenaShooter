using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform Player;
    //private float aggroRange = 15f;

    public int damage = 10;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector3.Distance(transform.position, Player.position) < aggroRange)  //if player is within aggro range
        //{
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(Player.position);
        //}
        //else
        //{
        //    navMeshAgent.isStopped = true;
        //}

        if (Vector3.Distance(transform.position, Player.position) < 2f)
        {
            Player.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
