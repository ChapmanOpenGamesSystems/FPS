/*
Name: Nick Lai
Student ID#: 2282417
Chapman email: lai137@mail.chapman.edu
Course Number and Section: 340-02
Assignment: Aim Rush

Contains logic for the pause menu and end screens
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject crosshair;

    public SoundManager sm;

    // Grabs SoundManager on Awake
    private void Awake()
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }

            else if (!isPaused)
            {
                Pause();
            }
        }
    }

    // Contains logic for resuming the game
    public void Resume()
    {
        sm.PlaySound(sm.buttonClick, 1f);
        pauseMenuUI.SetActive(false);
        crosshair.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Contains logic for pausing the game
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        crosshair.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Contains logic for returning to the Main menu
    public void BackToMainMenu()
    {
        sm.PlaySound(sm.buttonClick, 1f);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    // Contains logic for reloading the scene
    public void Retry()
    {
        sm.PlaySound(sm.buttonClick, 1f);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
