using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 7f;
    Rigidbody2D rb;

    float health = 100f;
    bool isGrounded = false;
    bool isAttacking = false;
    bool isCrouching = false;
   
    
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] Transform attackPos;
    [SerializeField] float attackDamage = 20f;
    
    
    [SerializeField] LayerMask groundLayer; 
    [SerializeField] LayerMask enemy;
    SpriteRenderer sprite;
    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float  horizontalInput = Input.GetAxis("Horizontal");
        if (!isCrouching &&  !isAttacking)
        {
            rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
        }
        
        if (horizontalInput > 0 && health > 0)
        {
            sprite.flipX = false;
            attackPos.localPosition = new Vector2(0.25f, attackPos.localPosition.y);
        }
        else if (horizontalInput < 0 && health > 0)
        {
            sprite.flipX = true;
            attackPos.localPosition = new Vector2(-0.25f, attackPos.localPosition.y);
        }

        //Storing the collider in a variable for easy use
        Collider2D col = GetComponent<Collider2D>();
        isGrounded = Physics2D.OverlapCircle(transform.position - transform.up * ((col.bounds.extents.y / transform.localScale.y - col.offset.y) * transform.localScale.y), 0.01f, groundLayer );

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetBool("Run", false);
        }



        if (horizontalInput != 0 && !isAttacking)
        {
            anim.SetBool("Run", true);
            anim.SetFloat("Y", 0);
        }
        else
        {
            anim.SetBool("Run", false);
        }
        

        anim.SetFloat("Y", rb.linearVelocity.y );
        if (isGrounded)
        {
            
            anim.SetFloat("Y", 0);
        }


        if (Input.GetButtonDown("Fire1") && !isAttacking && rb.linearVelocity.y == 0)
        {

            anim.SetBool("Attack", true);
            isAttacking = true;
            rb.linearVelocity = new Vector2(0, 0);
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
            foreach (Collider2D enemy in enemiesToDamage)
            {
                enemy.GetComponent<Enemy>().Changehealth(attackDamage);
            }

        }
        else if (isAttacking)
        {
            rb.linearVelocity = new Vector2(0, 0);
            Invoke("finishattack", 1.2f);
        }



        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool ("KeepCrouch", true);

            isCrouching = true;
            rb.linearVelocity = new Vector2(0, 0);


        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("KeepCrouch", false);
            Invoke("quitcrouch", 0.3f);
            
        }



        if (health <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            
        }

        
    }

    void quitcrouch()
    {
        isCrouching = false;
    }
    void finishattack()
    {
        anim.SetBool("Attack", false);
        isAttacking = false;
    }



    public void Changehealth(float amount)
    {
        health -= amount;
        print(health);
        if (health <= 0)
        {
            anim.SetBool("Dead", true);
            rb.linearVelocity = Vector2.zero;
            Invoke("Die", 0.77f);
            

        }


    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


    void Die()
    {
        anim.enabled = false;
        
    }
    
 





}
