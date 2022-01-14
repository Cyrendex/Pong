using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public PlayerBumper owner;
    GameHandler2P gameHandler;
    Ball ball;

    void Start()
    {
        AssignAppropriateGameHandler();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CollidedWithABall(collision))
            return;
        
        ball = collision.gameObject.GetComponent<Ball>();
        PlayerBumper lastCollidedBumper = ball.lastCollidedBumper;

        if (lastCollidedBumper != null)
        {
            PlayerBumper player = lastCollidedBumper.GetComponent<PlayerBumper>();

            if (PlayerScoredIntoOwnGoal(player))
            {
                if (NoOneHitTheBallBefore())
                    player.score--;
                else
                    GiveAPointToPreviousPlayer();
            }
            else
            {
                player.score++;
            }
        }
        else
        {
            Debug.Log("Nobody hit the ball!");
        }

        Destroy(ball.gameObject);
        gameHandler.ballCount--;

        gameHandler.UpdateScores();

        if (gameHandler.MoreBallsLeft())
            return;

        gameHandler.ResetPositions();
        gameHandler.DestroySpawnedGameObjects();
        gameHandler.InstantiateBall();
    }

    private bool PlayerScoredIntoOwnGoal(PlayerBumper player)
    {
        return owner == player;
    }

    private bool CollidedWithABall(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Split Ball");
    }

    private void AssignAppropriateGameHandler()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler2P>();
    }

    private bool NoOneHitTheBallBefore()
    {
        return ball.previousCollidedBumper == null;
    }

    private void GiveAPointToPreviousPlayer()
    {
        PlayerBumper previousPlayer = ball.previousCollidedBumper;
        previousPlayer.score++;
    }

    private bool CollidedWithASplitBall()
    {
        return ball.gameObject.CompareTag("Split Ball");
    }    
}
