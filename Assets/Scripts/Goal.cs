using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public PlayerBumper owner;
    GameHandler2P gameHandler;

    void Start()
    {
        AssignAppropriateGameHandler();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CollidedWithABall(collision))
            return;

        PlayerBumper lastCollidedBumper = collision.gameObject.GetComponent<Ball>().lastCollidedBumper;
        if (lastCollidedBumper != null)
        {
            PlayerBumper player = lastCollidedBumper.GetComponent<PlayerBumper>();

            if (PlayerScoredIntoOwnGoal(player))
            {
                player.score--;
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

        gameHandler.UpdateScores();
        gameHandler.ResetPositions();
        DestroyCloneBalls();
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

    private void DestroyCloneBalls()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Split Ball");

        if(clones.Length > 0)
            foreach (GameObject clone in clones)
            {
                Destroy(clone);
            }
    }
}
