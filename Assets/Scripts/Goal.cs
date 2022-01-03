using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    GameHandler2P gameHandler;

    void Start()
    {
        AssignAppropriateGameHandler();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (CollidedWithABall(collision))
        {
            BumperTrigger lastCollidedBumperTrigger = collision.gameObject.GetComponent<Ball>().lastCollidedBumperTrigger;
            if (lastCollidedBumperTrigger != null)
            {
                PlayerBumper player = lastCollidedBumperTrigger.GetComponentInParent<PlayerBumper>();
                player.score++;
                Debug.Log("Player " + player.bumperNumber + "'s score: " + player.score);                
            }
            else
            {
                Debug.Log("Nobody hit the ball!");
            }
            gameHandler.ResetPositions();
        }
    }

    private bool CollidedWithABall(Collider2D collision)
    {
        return collision.gameObject.CompareTag("Ball");
    }

    private void AssignAppropriateGameHandler()
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler2P>();
    }
}
