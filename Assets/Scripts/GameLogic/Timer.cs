/*
Name: Nick Lai
Student ID#: 2282417
Chapman email: lai137@mail.chapman.edu
Course Number and Section: 340-02
Assignment: Aim Rush

Uses a Coroutine as a countdown timer
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int timeLeft = 30;
    public Text countdownText;
    public bool timerIsOn = true;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("LoseTime");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsOn == true)
        {
            countdownText.text = timeLeft.ToString();

            //ends the game when time reaches zero
            if (timeLeft <= 0)
            {
                StopCoroutine("LoseTime");
            }
        }
    }

    // Counts down time remaining by 1 every second
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}
