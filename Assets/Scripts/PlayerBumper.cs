using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBumper : MonoBehaviour
{
    public int bumperNumber;
    public float speed = 5f;

    [NonSerialized]
    public int score;

    [NonSerialized]
    public float movement;

    [NonSerialized]
    public Material material;

    Vector3 startingPosition;    
    Rigidbody2D rb;
    string controls;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        material = GetComponent<Renderer>().material;
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
