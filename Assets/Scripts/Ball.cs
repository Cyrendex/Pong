using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] public float speed = 5.0f;
    [SerializeField] public float speedMultiplier = 1.15f;
    public Bumper lastCollidedBumper;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    void Update()
    {
        
    }

    private void LaunchBall()
    {
        float randomX = Random.Range(0, 2) == 0 ? -1 : 1;
        float randomY = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * randomX, speed * randomY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollidedObjectIsABumper(collision))
        {
            lastCollidedBumper = collision.gameObject.GetComponent<Bumper>();
            float transferredMomentum = lastCollidedBumper.movement;
            rb.velocity = new Vector2(rb.velocity.x * speedMultiplier, rb.velocity.y + (transferredMomentum * (rb.velocity.x / 4))); // Speeds up the ball and slightly alters y velocity depending on the bumper's movement.
        }
    }

    private bool CollidedObjectIsABumper(Collision2D collision)
    {
        return collision.gameObject.CompareTag("Bumper");
    }
}
