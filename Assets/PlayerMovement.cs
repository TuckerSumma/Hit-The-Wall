using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : MonoBehaviour
{
    public float jumpHeight = 10f;
    public Rigidbody2D rb;
    public float Speed;
    public static bool isFacingRight = true;
    private float horizontalInput;
    static public bool maxSpeed = false;
    int maxValue = 650;
    public static int score = 0;
    public SpikeSpawner spawnerRight;
    public LSpikeSpawner spawnerLeft;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Force();
    }

    public void Jump()
    {
        float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y * rb.gravityScale));
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
    }

    void Force()
    {
        if (isFacingRight == true) 
        {
            horizontalInput = 1f;
        }
        else
        {
            horizontalInput = -1f;
        }
        Vector2 moveDirection = new Vector2(horizontalInput * Speed * Time.deltaTime, rb.velocity.y);
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
            
        }
    }
}