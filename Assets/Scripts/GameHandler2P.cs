using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler2P : MonoBehaviour
{
    [Header("Ball")]
    public Ball ball;

    [Header("Players")]
    public PlayerBumper player1;
    public PlayerBumper player2;

    [Header("Scores")]
    public Text p1Score;
    public Text p2Score;

    void Start()
    {
        SetTextColors();
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

    public void UpdateScores()
    {
        p1Score.text = player1.score.ToString();
        p2Score.text = player2.score.ToString();
    }

    private void SetTextColors()
    {
        p1Score.color = player1.GetComponent<Renderer>().material.color;
        p2Score.color = player2.GetComponent<Renderer>().material.color;
    }
}
