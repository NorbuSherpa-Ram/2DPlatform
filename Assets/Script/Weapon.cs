using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float speed =10f;
    public float attackRange;
    public int damage;
    public Sprite weapon;

   

    void Update()
    {

        transform.Rotate (Vector3.left  * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag ("Player"))
        {
            collision.GetComponent<Player>().Equip(this);
           
            Destroy(gameObject );
        }
    }
}
