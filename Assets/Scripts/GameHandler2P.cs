using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler2P : MonoBehaviour
{
    [Header("Ball")]
    public Ball ball;

    [Header("Players")]
    public PlayerBumper player1;
    public PlayerBumper player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPositions()
    {
        ball.GetComponent<Ball>().ResetBall();
        player1.GetComponent<PlayerBumper>().ResetBumper();
        player2.GetComponent<PlayerBumper>().ResetBumper();
    }
}
