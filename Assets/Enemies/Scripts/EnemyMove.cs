using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Transform Player;

    [SerializeField] LayerMask groundLayer, playerLayer;
    //private float aggroRange = 15f;

    //Attacking

    //States
    [SerializeField] float attackRange;
    bool playerInAttackRange;
    
    public Animator animator;

    SphereCollider sphereCollider;
    public int damage = 10;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        sphereCollider = GetComponentInChildren<SphereCollider>();

    }

    private void AttackPlayer()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
        {
            animator.SetTrigger("Attacking");
            navMeshAgent.SetDestination(transform.position);       
        }
       
    }

    private void ChasePlayer()
    {
       navMeshAgent.SetDestination(Player.position);
    }

private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // Update is called once per frame
    void Update()
    {
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
            
      
        if (playerInAttackRange)
        {
            //make the enemy look at the player
            transform.LookAt(new Vector3(Player.position.x, transform.position.y, Player.position.z));
            
            
            AttackPlayer();
        }
        else
        {
            ChasePlayer();
        }
       
        
    }

    void EnableAttack()
    {
        sphereCollider.enabled = true;        
    }

    void DisableAttack()
    {
        sphereCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
           if(Player != null)
           {
            //add knockback
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
           }
           
        }
    }

}
