using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpikeCollision : MonoBehaviour
{
    public PlayerMovement Movement;
    public Button Restart;
    public Button Exit;
    public TextMeshProUGUI maxSpeed;
    static public bool Dead; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("FloorSpike"))
        {
            Movement.rb.isKinematic = true;
            Movement.rb.velocity = Vector2.zero;
            Movement.enabled = false;
            Restart.gameObject.SetActive(true);
            Exit.gameObject.SetActive(true);
            Dead = true;
            maxSpeed.enabled = false;
        }
    }
}
