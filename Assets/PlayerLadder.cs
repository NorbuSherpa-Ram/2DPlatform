using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadder : MonoBehaviour
{
    [Header("LadderInfo")]
    public float length = 0.5f;
    public float speed = 0.5f;
    public LayerMask whatIsLadder;

    public Rigidbody2D rb;
    bool isClaming;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, length, whatIsLadder);
        if (hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                isClaming = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightApple) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                isClaming = false;
        }

        if (isClaming == true && hitInfo.collider != null)
        {
            float verInput = Input.GetAxis("Vertical");

            rb.velocity = new Vector2(rb.position.x, verInput * speed*Time.deltaTime );
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 9.86f;
        }
       
    }
   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector2.up * length);
    }
}
