using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI MaxSpeed;
    public TextMeshProUGUI Score;
    public PlayerMovement Player;
    public bool gameStart = false;
    public TextMeshProUGUI Start;
    public AudioMixer audioMixer;
    
    void Update()
    {
        Score.text = PlayerMovement.score.ToString();

        if (PlayerMovement.maxSpeed == true)
        {
            MaxSpeed.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && gameStart == false ||Input.GetMouseButtonDown(0) && gameStart == false)
        {
            Player.enabled = true;
            Score.enabled = true;
            Player.rb.isKinematic = false;
            Start.enabled = false;
            gameStart = true;
            Player.Jump();
            audioMixer.SetFloat("LowPass", 22000);
        }
        if (SpikeCollision.Dead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {      
                Restart();
            }
        }
    }
    void Restart()
    {
        ResetValues(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NewGameButtion()
    {
        Restart();
    }

    private void ResetValues()
    {
        PlayerMovement.score = 0;
        SpikeSpawner.numberOfSpikes = 5;
        PlayerMovement.isFacingRight = true;
        PlayerMovement.maxSpeed = false;
        SpikeCollision.Dead = false;
    }

    public void Exit()
    {
        ResetValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
