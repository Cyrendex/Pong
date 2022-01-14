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

    [NonSerialized]
    public PlayerBumper previousCollidedBumper;

    [NonSerialized]
    public TrailRenderer trailRenderer;

    [SerializeField]
    private Material defaultTrailMaterial;


    private Vector3 startingPosition;
    private Rigidbody2D rb;    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.material = defaultTrailMaterial;
        LaunchBall();
    }

    private void LaunchBall()
    {
        float randomX = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        float randomY = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * randomX, speed * randomY);
    }

    private void OnCollisionEnter2D(Collision2D collidedObject)
    {
        if (CollidedObjectIsABumper(collidedObject))
        {
            PlayerBumper collidedBumper = collidedObject.gameObject.GetComponent<PlayerBumper>();

            if (BallDidntCollideWithSameBumper(collidedBumper))             
                if (PreviousBumperIsntLastBumper())
                    previousCollidedBumper = lastCollidedBumper;               

            lastCollidedBumper = collidedBumper;

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
        trailRenderer.material = defaultTrailMaterial;
        transform.position = startingPosition;
        lastCollidedBumper = null;
        previousCollidedBumper = null;
        LaunchBall();
    }

    private void SetTrailColorToBumperColor()
    {
        trailRenderer.material = lastCollidedBumper.material;
    }

    private void LimitBallSpeed()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private bool BallDidntCollideWithSameBumper(PlayerBumper collidedBumper)
    {
        return lastCollidedBumper != collidedBumper;
    }

    private bool PreviousBumperIsntLastBumper()
    {
        return previousCollidedBumper != lastCollidedBumper;
    }    
}
