using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootFire : MonoBehaviour
{
    Transform player;
    Vector3 target;
    public float speed;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        target = new Vector3(player.position.x, player.position.y);
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.magnitude == target.magnitude )
        {
            Destroy(this.gameObject );
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag ("Weapon"))
        {
            return;
        }

        if (collision.gameObject.layer ==LayerMask.NameToLayer   ("Enemy"))
        {
            collision.gameObject.GetComponent<ShootingEnemy>().TakeDamage(1);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.CompareTag ("Player"))
        {
            collision.GetComponent<Player>().Damage(1);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
