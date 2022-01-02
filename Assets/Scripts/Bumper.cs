using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] public int bumperNumber;
    [SerializeField] public float speed = 5f;
    Rigidbody2D rb;

    string verticalControls;
    float movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        verticalControls = "Vertical" + bumperNumber;
    }

    private void ReadMovement()
    {
        movement = Input.GetAxisRaw(verticalControls);
    }

    private void Move()
    {
        rb.velocity = new Vector2(rb.velocity.x, movement * speed);
    }
}
