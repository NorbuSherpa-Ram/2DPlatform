using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("PlayerDamage")]
    [SerializeField] int damage;

    [Header("Health")]
    [SerializeField] int health = 1;

    [Header ("Effect")]
    public GameObject blood;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Damage(damage);
        }
    }
    public virtual void TakeDamage(int damage )
    {
        health -= damage;
        if(health<=0)
        {
            Destroy(gameObject );
        }
        Instantiate(blood, transform.position, Quaternion.identity);
    }
    public virtual void  Attack()
    {
        // Attack 
    }
}
