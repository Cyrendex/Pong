using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    Vector3 ballPosition;

    [NonSerialized]
    public int ballCount;


    public bool enabledPowerUps;
    GameObject[] allPowerUps;
    Vector3 center;

    [SerializeField]
    Vector2 size;

    [SerializeField]
    float powerUpSpawnInterval;


    void Start()
    {
        center = transform.position;
        ballCount = GameObject.FindGameObjectsWithTag("Ball").Length;
        SetTextColors();
        ballPosition = new Vector3(0, 0, 0);

        if (enabledPowerUps)
        {
            allPowerUps = Resources.LoadAll<GameObject>("PowerUps");
            StartCoroutine(PowerUpCycle());
        }            
    }

    public void ResetPositions()
    {
        player1.ResetBumper();
        player2.ResetBumper();
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

    private void DestroyPowerUps()
    {
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Power Up");

        if (powerups.Length > 0)
            foreach (GameObject powerup in powerups)
            {
                Destroy(powerup);
            }
    }

    public void DestroySpawnedGameObjects()
    {
        DestroyPowerUps();
    }

    public void InstantiateBall()
    {
        Instantiate(ball, ballPosition, Quaternion.identity);
    }

    public bool MoreBallsLeft()
    {
        return ballCount > 0;
    }

    void SpawnRandomPowerUp()
    {
        float posX, posY;
        Vector3 position;

        posX = UnityEngine.Random.Range(-size.x / 2, size.x / 2);
        posY = UnityEngine.Random.Range(-size.y / 2, size.y / 2);

        position = new Vector3(posX, posY, 0);

        int powerUpIndex = UnityEngine.Random.Range(0, allPowerUps.Length);

        Instantiate(allPowerUps[powerUpIndex], position, Quaternion.identity);
    }

    IEnumerator PowerUpCycle()
    {
        SpawnRandomPowerUp();
        yield return new WaitForSeconds(powerUpSpawnInterval);
        StartCoroutine(PowerUpCycle());
    }
}
