using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBall : MonoBehaviour
{
    GameHandler2P gameHandler;
    
    PlayerBumper lastCollidedBumper;

    Ball splitBall;
    Rigidbody2D rbSplit;

    Ball mainBall;
    Rigidbody2D rbMain;

    float offsetAmount = 1.5f;

    private void Awake()
    {
        AssignAppropriateGameHandler();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CollidedWithABall(collision))
            return;

        mainBall = collision.gameObject.GetComponent<Ball>();
        lastCollidedBumper = mainBall.lastCollidedBumper;
        rbMain = mainBall.GetComponent<Rigidbody2D>();

        // Change the spawn position to avoid collision
        Vector3 pos = ReturnSplitBallPosition();

        splitBall = Instantiate(mainBall, pos, Quaternion.identity);
        CopyMainBallToClone();
        splitBall.tag = "Split Ball";
        gameHandler.ballCount++;
        
        rbSplit = splitBall.GetComponent<Rigidbody2D>();       
        AdjustSplitBallVelocity();

        Destroy(gameObject);
    }

    private bool CollidedWithABall(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Split Ball");
    }

    private void CopyMainBallToClone()
    {
        splitBall.lastCollidedBumper = lastCollidedBumper;        
        if(lastCollidedBumper != null)
            splitBall.trailRenderer.material = lastCollidedBumper.material;
    }

    private bool BumperHasVerticalControls()
    {
        return lastCollidedBumper.bumperNumber == 1 || lastCollidedBumper.bumperNumber == 2;
    }

    private bool BallWasntHitByABumper()
    {
        return lastCollidedBumper == null;
    }
    private void AssignAppropriateGameHandler()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler2P>();
    }

    /*
     * The adjustments are done by the following logic:
     * 
     * -If the ball wasn't hit by a bumper, both x and y components are reversed to prevent unfair advantage.
     * -If the ball is hit by a bumper that has vertical controls, the y velocity is reversed and the ball is shifted to the opposite of main ball's y direction to avoid collisions.
     * -If the ball is hit by a bumper that has horizontal controls, the x velocity is reversed and the ball is shifted to the opposite of main ball's x direction to avoid collisions.
     * 
     */

    private void AdjustSplitBallVelocity()
    {
        Vector2 newVelocity;

        if (BallWasntHitByABumper())
            newVelocity = new Vector2(rbMain.velocity.x * -1, rbMain.velocity.y * -1);
        else if (BumperHasVerticalControls())
            newVelocity = new Vector2(rbMain.velocity.x, rbMain.velocity.y * -1);
        else
            newVelocity = new Vector2(rbMain.velocity.x * -1, rbMain.velocity.y);

        rbSplit.velocity = newVelocity;
    }

    private Vector3 ReturnSplitBallPosition()
    {
        Vector3 splitPosition;
        Vector3 mainPosition = mainBall.transform.position;

        float offsetX = (rbMain.velocity.x >= 0 ? -1 : 1) * offsetAmount;
        float offsetY = (rbMain.velocity.y >= 0 ? -1 : 1) * offsetAmount;

        if (BallWasntHitByABumper())
            splitPosition = new Vector3(mainPosition.x + offsetX, mainPosition.y + offsetY, 0);
        else if (BumperHasVerticalControls())
            splitPosition = new Vector3(mainPosition.x, mainPosition.y + offsetY, 0);
        else
            splitPosition = new Vector3(mainPosition.x + offsetX, mainPosition.y, 0);

        return splitPosition;
    }
}
