using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollidedWithABall(collision))
        {
            Bumper lastCollidedBumper = collision.gameObject.GetComponent<Ball>().lastCollidedBumper;
            if (Bumper != null) { }
        }
    }

    private bool CollidedWithABall(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Ball");
    }
}
