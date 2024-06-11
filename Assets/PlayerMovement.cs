using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : MonoBehaviour
{
    public float jumpHeight = 10f;
    public Transform transfromRB;
    public Rigidbody2D rb;
    public float Speed;
    public static bool isFacingRight = true;
    private float horizontalInput;
    static public bool maxSpeed = false;
    int maxValue = 650;
    public static int score = 0;
    public SpikeSpawner spawnerRight;
    public LSpikeSpawner spawnerLeft;
    float angleInDegrees;
    float angleInRadians;

    private void Awake()
    {
        horizontalInput = 1f;
        Application.targetFrameRate = 165;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||Input.GetMouseButtonDown(0))
        {
            Jump();
        }
       
    }

    void FixedUpdate()
    {
        angleInRadians = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        rb.rotation = 90+angleInDegrees;
        Debug.Log(rb.rotation);
        //Force();
    }

    public void Jump()
    {
        float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y * rb.gravityScale));
        rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpVelocity);
    }

    public void Force()
    {
        UnityEngine.Vector2 moveDirection = new UnityEngine.Vector2(horizontalInput * Speed * 0.02f, rb.velocity.y);
        rb.velocity = moveDirection;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            spawnerRight.DestroySpike();
            score++;
            if (Speed < maxValue - 5)
            {
                Speed += 6.5f;
            }
            else if (maxSpeed == false)
            {
                maxSpeed = true;
            }

            if (isFacingRight)
            {
                isFacingRight = false;
                spawnerLeft.SpawnSpikes();
            }
            else
            {
                isFacingRight = true;
                spawnerRight.SpawnSpikes();
            }
            if (isFacingRight == true)
            {
                horizontalInput = 1f;
            }
            else
            {
                horizontalInput = -1f;
            }
            Force();
        }
    }
}