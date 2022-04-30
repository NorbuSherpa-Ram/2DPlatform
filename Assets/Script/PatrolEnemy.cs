using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : Enemy
{
    [SerializeField] Transform[] patrolPoints;
    int currentPatrolPoint;
    [SerializeField] float speed;

    [Header("WaitTime")]
    public float startWaitTime = 0.5f;
    float waitTime;

    [Header("Attack")]
    public float attackRange = 2f;
    public Transform player;
    float distance;

    Animator animator;

    void Start()
    {
        waitTime = startWaitTime;
        currentPatrolPoint = 0;
        transform.position = patrolPoints[currentPatrolPoint].position;
        transform.rotation = patrolPoints[currentPatrolPoint].rotation;
      
        player = GameObject.Find("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            distance = Vector2.Distance(player.position, transform.position);
        }
        if ( distance <= attackRange)
        {
            animator.SetTrigger("attack");
            animator.SetBool("move", false);
        }
        else
        {
            animator.SetBool("move", true );
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolPoint].position, speed * Time.deltaTime);
        }

        if (transform.position == patrolPoints[currentPatrolPoint].position)
        {
            animator.SetBool("move", false);
            transform.rotation = patrolPoints[currentPatrolPoint].rotation;
            if (waitTime <= 0)
            {
                if (currentPatrolPoint + 1 < patrolPoints.Length)
                {
                    currentPatrolPoint++;
                }
                else
                {
                    currentPatrolPoint = 0;
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
    
        Gizmos.DrawWireSphere(transform .position, attackRange);
    }
}
