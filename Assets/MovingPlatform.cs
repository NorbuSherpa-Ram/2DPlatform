using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    int index;
    public float speed = 1f;
    public Transform[] patrolPoints;

    [Header("WaitTime")]
    public float startWaitTime = 0.5f;
    private float waitTime;
    private void Start()
    {
        transform.position = patrolPoints[0].position;
        waitTime = startWaitTime;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[index].position, speed * Time.deltaTime);
        if (transform.position == patrolPoints[index].position)
        {
            if (waitTime <= 0)
            {
                if (index + 1 < patrolPoints.Length)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other )
    {
        if(other.gameObject.CompareTag ("Player"))
        {
            other.gameObject.transform.parent = transform;
        }
    } 
    private void OnCollisionExit2D(Collision2D other )
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.parent = null ;
        }
    }
}
