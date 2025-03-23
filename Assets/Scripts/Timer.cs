using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// timer was based on the script from:
// https://www.wayline.io/blog/how-to-make-a-countdown-timer-in-unity

public class Timer : MonoBehaviour
{
    private bool timerActive;
    private float timeLeft;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeLeft = 15f + 5*Time.deltaTime;
        timerActive = false;
        manager.startTimer.AddListener(ActivateTimer);
        manager.resetTimer.AddListener(ResetTimer);
        manager.pauseTimer.AddListener(PauseTimer);
    }

    public void ActivateTimer()
    {
        timerActive = true;
    }

    public void ResetTimer()
    {
        timeLeft = 15f + 5 * Time.deltaTime;
        timerActive = false;

        // Divide the time by 60
        float minutes = Mathf.FloorToInt(timeLeft / 60);

        // Returns the remainder
        float seconds = Mathf.FloorToInt(timeLeft % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        // Set the text string
        timerText.SetText(timeString);
    }

    public void PauseTimer()
    {
        timerActive = false;
        timerText.SetText("Timer paused");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive && timeLeft - Time.deltaTime > 0)
        {
            // Subtract elapsed time every frame
            timeLeft -= Time.deltaTime;

            // Divide the time by 60
            float minutes = Mathf.FloorToInt(timeLeft / 60);

            // Returns the remainder
            float seconds = Mathf.FloorToInt(timeLeft % 60);

            string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            // Set the text string
            timerText.SetText(timeString);

        }

        if (!(timeLeft - Time.deltaTime > 0)) {
            timerText.SetText("Time's up!");
        }
    }
}
