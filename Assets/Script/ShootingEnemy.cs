using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [Header ("Shoot Enemy")]
    public float speed;
    public float shootRange;
    public float retreatRange;
    public float chestRange;

    [Header("Shoot Time")]
    public float timeToShoot;
    float timeBetweenShoot;

    public GameObject firePrefabs;
    public Transform  attackPoint;

    GameObject player;
    public bool isFacingLeft= true ;
    Vector2 posiotn;
    void Start()
    {
        timeBetweenShoot = timeToShoot;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Flip();
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > shootRange && distance <= chestRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (distance <= shootRange && distance >retreatRange )
        {
            transform.position = this.transform.position;
            Shoot();
        }
        else if (distance < shootRange && distance <= retreatRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
        }
    }
    void Flip()
    {
        posiotn  = transform.position - player.transform.position;
        if (posiotn.x  <= transform.position .x && isFacingLeft)
        {
            transform.localScale = new Vector2(-1*transform.localScale .x, transform.localScale.y);
            isFacingLeft = false  ;
        }
        else if (posiotn.x  > transform.position .x && !isFacingLeft)
        {
            transform.localScale = new Vector2( -1*transform.localScale.x, transform.localScale.y);
            isFacingLeft = true  ;
        }
    }
    void Shoot()
    {
        if(timeToShoot <=0)
        {
            Instantiate(firePrefabs, attackPoint.position, Quaternion.identity);
            timeToShoot = timeBetweenShoot;
        }
        else
        {
            timeToShoot -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chestRange);
        Gizmos.DrawWireSphere(transform.position, shootRange);
        Gizmos.DrawWireSphere(transform.position, retreatRange );
    }
}
