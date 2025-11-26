using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 7f;
    Rigidbody2D rb;
    bool isGrounded = false;
    bool isAttacking = false;

    [SerializeField] LayerMask groundLayer; 
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
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
        if (horizontalInput > 0)
        {
            sprite.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            sprite.flipX = true;
        }
        //Storing the collider in a variable for easy use
        Collider2D col = GetComponent<Collider2D>();
        isGrounded = Physics2D.OverlapCircle(transform.position - transform.up * ((col.bounds.extents.y / transform.localScale.y - col.offset.y) * transform.localScale.y), 0.01f, groundLayer );

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }



        if (horizontalInput != 0)
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


        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {

            anim.SetBool("Attack", true);
            isAttacking = true;

            

        }
        else if (isAttacking)
        {
            anim.SetBool("Attack", false);
            isAttacking = false;
        }


    crouch();

        
    }

    void crouch()
    {
        //float  verticalInput = Input.GetAxis("Vertical");
       // if (verticalInput < 0) { anim.SetBool("Crouch", true); }

        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool ("Crouch", true);

        }


    }




}
