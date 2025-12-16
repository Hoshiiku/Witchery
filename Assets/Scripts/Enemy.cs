using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    
    [SerializeField] protected float health = 50f;
    [SerializeField] protected float damage = 10f;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected SpriteRenderer sprite;
    bool dead = false;
    protected GameObject player;
    protected float attackCooldown = 1.5f;
    protected float distance;
    protected float timer;
    [SerializeField] protected float attackrange = 1.5f;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerControl>().gameObject;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        
    }

    // Update is called once per frame
    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (!dead)
        {
            Attack();
        }

        
        
    }
    private void FixedUpdate()
    {
        if (!dead)
        {
            Move();
        }
        else
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
    }



    public virtual void Move()
    {
        
    }
    public virtual void Attack()
    {
        
    }




    public void Changehealth(float count)
    {
        health -= count;
        print(health);
        if (health <= 0)
        {
            dead = true;

            
            anim.SetBool("Die", true);
            anim.SetBool("Attack", false);
            anim.SetBool("Walk", false);

        }
    }
    public void Die()
    {
            
        
        Destroy(gameObject);    
    
    
    
    }




}
