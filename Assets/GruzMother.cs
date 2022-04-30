using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruzMother : Enemy 
{
    Rigidbody2D enemyRb;

    [Header("idel")]
    [SerializeField] float idelSpeed;
    [SerializeField] Vector2 idelDirection;

    [Header("AttackUpAndDown")]
    [SerializeField] float attackSpeed;
    [SerializeField] Vector2 attackDirection;

    [Header("AttackPlayer ")]
    [SerializeField] float playerAttackSpeed;
    [SerializeField] Transform playerPos;
    bool hasPlayerPosition;
    Vector3 targetPos;

    [Header("GroundCheck ")]
    [SerializeField] Transform groundCheckUp;
    [SerializeField] Transform groundCheckDown;
    [SerializeField] Transform groundCheckFront;
    [SerializeField] float groundCheckRadious;
    [SerializeField] LayerMask groundLayer;
    bool isTouchingUp;
    bool isTouchingDown;
    bool isTouchingFront;

    bool goingUp = true;
    bool isFacingLeft = true;


    public Animator animator;


    private void Start()
    {
        idelDirection.Normalize();
        attackDirection.Normalize();
        enemyRb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadious, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadious, groundLayer);
        isTouchingFront = Physics2D.OverlapCircle(groundCheckFront.position, groundCheckRadious, groundLayer);
       // FlipTowardPlayer();
    }
    public override void TakeDamage(int damage)
    {
       
    }
   public  void RandomState()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            animator.SetTrigger("upNDown");
        }
        else if (random == 1)
        {
            animator.SetTrigger("attackPlayer");
        }

    }

    public void IdelState()
    {
        if (goingUp && isTouchingUp)
        {
            ChangeDirection();
        }
        if (!goingUp && isTouchingDown)
        {
            ChangeDirection();
        }
        if (isTouchingFront)
        {
            if (isFacingLeft)
            {
                Flip();
            }
            else if (!isFacingLeft)
            {
                Flip();
            }
        }
        enemyRb.velocity = idelSpeed * idelDirection;
        animator.ResetTrigger("slamed");
    }

    public void AttackUpAndDownState()
    {
        if (goingUp && isTouchingUp)
        {
            ChangeDirection();
        }
        if (!goingUp && isTouchingDown)
        {
            ChangeDirection();
        }
        if (isTouchingFront)
        {
            if (isFacingLeft)
            {
                Flip();
            }
            else if (!isFacingLeft)
            {
                Flip();
            }
        }
        enemyRb.velocity = attackSpeed * attackDirection ;
    }

    public void AttackPlayer()
    {
        if (!hasPlayerPosition)
        {
            targetPos = (playerPos.position - transform.position).normalized ;
            hasPlayerPosition = true;
        }
        else if (hasPlayerPosition)
        {
            enemyRb.velocity = targetPos * playerAttackSpeed;
        }
        if (isTouchingDown || isTouchingFront)
        {
            enemyRb.velocity = Vector2.zero;
            hasPlayerPosition = false;
                animator.SetTrigger("slamed");
        }
    }

    void ChangeDirection()
    {
        goingUp = !goingUp;
        idelDirection.y *= -1;
        attackDirection.y *= -1;
    }
  public   void FlipTowardPlayer()
    {
        float playerDirection = playerPos.position.x - transform.position.x;
        if (playerDirection >1 && isFacingLeft)
        {
            Flip();
        }
        else if (playerDirection < -1 && !isFacingLeft)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingLeft = !isFacingLeft;
        transform.Rotate(0, 180, 0);
        idelDirection.x *= -1;
        attackDirection.x *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheckUp.position, groundCheckRadious);
        Gizmos.DrawWireSphere(groundCheckDown.position, groundCheckRadious);
        Gizmos.DrawWireSphere(groundCheckFront.position, groundCheckRadious);
    }
}
