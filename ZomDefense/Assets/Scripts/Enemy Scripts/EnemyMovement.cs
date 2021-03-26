using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    public Animator animator;

    NavMeshAgent agent;
    private GameObject target;

    private float targetRange;
    public bool isAttacking = false;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Barricade");
    }

    private void Update()
    {
        if (!animator.GetBool("isDead") && target.GetComponent<Barricade>().getHealth() > 0) // if the enemy and player is alive, do things, otherwise stop movement
        {
            targetRange = Vector3.Distance(transform.position, target.transform.position);

            if (targetRange > gameObject.GetComponent<EnemyAttack>().attackRange)
            {
                agent.isStopped = false;
                GoToTarget();
            }
            else
            {
                agent.isStopped = true;

                if (!isAttacking) // if coroutine is already running, dont run another one
                    StartCoroutine(gameObject.GetComponent<EnemyAttack>().Attack());
            }
        }
        else
        {
            agent.isStopped = true;

            if (target.GetComponent<Barricade>().getHealth() <= 0) // if the player is dead, stop attacking and go to idle animation
            {
                animator.SetBool("targetDead", true);
                animator.SetBool("isAttacking", false);
            }
                
        }
            
    }

    private void GoToTarget()
    {
        animator.SetBool("isMoving", true);
        animator.SetBool("isAttacking", false);
        agent.SetDestination(target.transform.position);
    }

    public GameObject getTarget()
    {
        return target;
    }
}
