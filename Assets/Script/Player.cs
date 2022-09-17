using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] Animator animator;

    public float speed = 100;
    public float jumpForce = 100;

    [Header("GroundCheck")]
    public float radious = 0.5f;
    [SerializeField] bool isGround;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform groundCheck;

    [Header("FrontCheck")]
    public float wallSlideForce = 5f;
    public float frontCheckRadious = 0.5f;
    [SerializeField] bool isTouchingFront;
    [SerializeField] bool wallSliding;
    [SerializeField] Transform frontCheck;

    [Header("WallJump")]
    public bool wallJump;
    public float yForce = 3f;
    public float xforce = 5f;
    public float wallJumpTime = .5f;

    [Header("FLip")]
    [SerializeField] bool facingRight = true;

    [Header("Health")]
    public int health;

    [Header("Attack ")]
    public int damage = 1;
    public float timeBtwAttack = 1;
    private float nextAttackTime;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask whatIsEnemy;

    public LayerMask whatIsBullate;

    [Header("Weapon")]
    public SpriteRenderer weponRendere;

    [Header("Effects")]
    public GameObject blood;
    public GameObject landing;
    public ParticleSystem slideEffect;
    public GameObject pickUpEffect;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hurt;
    public AudioClip jump;
    public AudioClip attack;
    public AudioClip pickUp;
    public AudioClip enemyHurt;
    public AudioClip bullateReturnClip;
  //  public AudioClip death;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Waking 
        float horInput = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(horInput * speed, playerRb.velocity.y);

        if (horInput > 0 && !facingRight)
        {
            FlipPlayer();
        }
        else if (horInput < 0 && facingRight)
        {
            FlipPlayer();
        }

        // Attack Animation 
        if (Time.time > nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlaySound(attack);
                FindObjectOfType<CameraShake>()?.ShakeCamera();
                animator.SetTrigger("attack");
                nextAttackTime = Time.time + timeBtwAttack;
            }
        }
        isGround = Physics2D.OverlapCircle(groundCheck.position, radious, whatIsGround);
        // Runing Animation and jumping Animation 
        if (isGround)
        {
            animator.SetBool("jump", false);
            animator.SetFloat("run", Mathf.Abs(horInput));

        }
        else
        {
            animator.SetBool("jump", true);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (isGround)
            {
                isGround = false;
                PlaySound(jump);
                playerRb.velocity = Vector2.up * jumpForce;
                //  Instantiate(landing , groundCheck.position, Quaternion.identity);
            }

        }
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, frontCheckRadious, whatIsGround);
        if (isTouchingFront && !isGround && horInput != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
            animator.SetBool("slide", false);
            slideEffect.Stop();
        }
        if (wallSliding)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, Mathf.Clamp(playerRb.velocity.y, -wallSlideForce, float.MaxValue));
            animator.SetBool("slide", true);
            slideEffect.Play();
        }
        if (Input.GetKeyDown(KeyCode.W) && wallSliding)
        {
            wallJump = true;
            Invoke("SetWallJumpFalse", wallJumpTime);
        }
        if (wallJump)
        {
            playerRb.velocity = new Vector2(xforce * -horInput, yForce);
            // FlipPlayer();
        }

    }
    // calling after  wallJumpTime on wall jump
    void SetWallJumpFalse()
    {
        wallJump = false;
    }
    void FlipPlayer()
    {
        ///   transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        playerRb.transform.Rotate(transform.rotation.x, 180, transform.rotation.z);
        facingRight = !facingRight;
    }
    public void Attack()
    {
        Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsEnemy);
        Collider2D[] bullateToReturn = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsBullate);
        foreach (Collider2D enemy in enemyToDamage)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
            PlaySound(enemyHurt);
        }     
        foreach (Collider2D bullate in bullateToReturn )
        {
            bullate.GetComponent<shootFire>().speed -= bullate.GetComponent<shootFire>().speed*2;
            PlaySound(bullateReturnClip );
        }
    }

    public void Damage(int damage)
    {
        PlaySound(hurt);
        health -= damage;
        if (health == 0)
        {
            //  PlaySound(death);
            Destroy(gameObject);
            //this.gameObject.SetActive(false);
        }
        FindObjectOfType<CameraShake>()?.ShakeCamera();
        Instantiate(blood, transform.position, Quaternion.identity);
    }
    public void Equip(Weapon weapon)
    {
        PlaySound(pickUp); 
        damage = weapon.damage;
        attackRange = weapon.attackRange;
        weponRendere.sprite = weapon.weapon;
        Instantiate(pickUpEffect, weapon.transform .position, Quaternion.identity);
    }

    void PlaySound(AudioClip clip)
    {
        //audioSource.clip = clip;
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radious);
        Gizmos.DrawWireSphere(frontCheck.position, frontCheckRadious);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
