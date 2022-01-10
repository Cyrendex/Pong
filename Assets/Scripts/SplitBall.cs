using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CollidedWithABall(collision))
            return;

        Ball mainBall = collision.gameObject.GetComponent<Ball>();
        Rigidbody2D rbMain = mainBall.GetComponent<Rigidbody2D>();

        // Change the spawn position to avoid collision
        Vector3 pos = new Vector3(mainBall.transform.position.x, mainBall.transform.position.y + 1.5f, 0);

        mainBall.trailRenderer.material = mainBall.lastCollidedBumper.material;
        Ball splitBall = Instantiate(mainBall, pos, Quaternion.identity);
        splitBall.tag = "Split Ball";
        splitBall.GetComponent<TrailRenderer>().material = mainBall.lastCollidedBumper.material;

        Rigidbody2D rbSplit = splitBall.GetComponent<Rigidbody2D>();

        // Reverse the velocity on y-axis
        rbSplit.velocity = new Vector2(rbMain.velocity.x, rbMain.velocity.y * -1);

        Destroy(gameObject);
    }

    private bool CollidedWithABall(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Split Ball");
    }

    private void CopyTrailMaterialToClone(Ball mainBall, Ball splitBall)
    {
        // a
    }
}
