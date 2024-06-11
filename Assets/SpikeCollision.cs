using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SpikeCollision : MonoBehaviour
{
    public PlayerMovement Movement;
    public Button Restart;
    public Button Exit;
    public TextMeshProUGUI maxSpeed;
    static public bool Dead;
    public Animator player;
    public AudioMixer audioMixer;
    public AudioClip deathSound;
    private AudioSource audioSource;
    static int highScore = 0;
    public TextMeshProUGUI bigHighScore;
    public TextMeshProUGUI littleHighScore;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
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
            audioMixer.SetFloat("LowPass", 300);
            audioSource.clip = deathSound;
            audioSource.Play();
            if (PlayerMovement.score > highScore)
            {
                highScore = PlayerMovement.score;
            }
            if (highScore >= 15 && highScore == PlayerMovement.score)
            {
                bigHighScore.gameObject.SetActive(true);
            }
            else
            {
                littleHighScore.gameObject.SetActive(true);
                littleHighScore.text = "HIGH SCORE: " + highScore;
            }
        }
    }
}
