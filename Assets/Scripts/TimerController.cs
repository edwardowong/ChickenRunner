using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;
    public TextMeshProUGUI timeCounter;
    public GameObject pauseMenuUI;
    public GameObject winUI;
    private TimeSpan playtime;
    private bool timerOn;
    private bool gamePaused = false;
    private float elapsedTime;
    public static float finalTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter.text = "Time: 00:00.00";
        timerOn = true;
        pauseMenuUI.SetActive(false);
        winUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //If the game is already paused
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (timerOn)
        {
            elapsedTime += Time.deltaTime;
            playtime = TimeSpan.FromSeconds(elapsedTime);
            string timeStr = "Time: " + playtime.ToString("mm':'ss'.'ff");
            timeCounter.text = timeStr;
        }
    }    

    public void Resume()
    {
        //Resume game
        timerOn = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }
    public void Pause()
    {
        //Pause game
        timerOn = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timerOn = false;
            winUI.SetActive(true);
            finalTime = elapsedTime;
        }
    }
}
