using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5.0f;
    public float speedMultiplier = 1.15f;
    public float maxSpeed = 10f;

    [NonSerialized]
    public PlayerBumper lastCollidedBumper;

    [SerializeField]
    private Material defaultTrailMaterial;

    private TrailRenderer trailRenderer;
    private Vector3 startingPosition;
    private Rigidbody2D rb;    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.material = defaultTrailMaterial;
        startingPosition = transform.position;
        LaunchBall();
    }

    private void LaunchBall()
    {
        float randomX = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        float randomY = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * randomX, speed * randomY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollidedObjectIsABumper(collision))
        {
            
            lastCollidedBumper = collision.gameObject.GetComponent<PlayerBumper>();
            float transferredMomentum = lastCollidedBumper.movement;
            rb.velocity = new Vector2(rb.velocity.x * speedMultiplier, rb.velocity.y + (transferredMomentum * (Mathf.Abs(rb.velocity.x) / 4))); // Speeds up the ball and slightly alters y velocity depending on the bumper's movement.
            LimitBallSpeed();
            SetTrailColorToBumperColor();
        }
    }

    private bool CollidedObjectIsABumper(Collision2D collision)
    {
        return collision.gameObject.CompareTag("Bumper");
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        GetComponent<TrailRenderer>().material = defaultTrailMaterial;
        transform.position = startingPosition;
        lastCollidedBumper = null;
        LaunchBall();
    }

    private void SetTrailColorToBumperColor()
    {
        trailRenderer.material = lastCollidedBumper.GetComponent<Renderer>().material;
    }

    private void LimitBallSpeed()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
