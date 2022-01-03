using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollidedWithABall(collision))
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            ball.lastCollidedBumperTrigger = this.gameObject.GetComponent<BumperTrigger>();
        }
    }
    private bool CollidedWithABall(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Ball");
    }
}
