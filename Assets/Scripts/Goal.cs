using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollidedWithABall(collision))
        {
            BumperTrigger lastCollidedBumperTrigger = collision.gameObject.GetComponent<Ball>().lastCollidedBumperTrigger;
            if (lastCollidedBumperTrigger != null)
            {

            }
        }
    }

    private bool CollidedWithABall(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Ball");
    }
}
