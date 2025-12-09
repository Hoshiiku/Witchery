using UnityEngine;

public class BringerOfDeath : Enemy
{
    [SerializeField] protected float speed = 3f;
    [SerializeField] protected float detectionRange = 10f;
    



    public override void Move()
    {
        if (distance < detectionRange)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
            anim.SetBool("Walk", true);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("Walk", false);

        }


        if (player.transform.position.x < transform.position.x)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        Changehealth(damageAmount);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
