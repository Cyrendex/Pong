using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBumper : MonoBehaviour
{
    [SerializeField] 
    public int bumperNumber;

    [SerializeField]
    public float speed = 5f;

    Vector3 startingPosition;
    public int score;
    Rigidbody2D rb;
    string controls;
    public float movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        AssignCorrectControls();
    }

    void Update()
    {
        ReadMovement();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void AssignCorrectControls()
    {
        controls = "Player" + bumperNumber;
    }

    private void ReadMovement()
    {
        movement = Input.GetAxisRaw(controls);
    }

    private void Move()
    {
        rb.velocity = new Vector2(rb.velocity.x, movement * speed);
    }

    public void ResetBumper()
    {
        rb.velocity = Vector2.zero;
        transform.position = startingPosition;
    }
}
