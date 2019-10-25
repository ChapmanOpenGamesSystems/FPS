/*
Name: Nick Lai
Student ID#: 2282417
Chapman email: lai137@mail.chapman.edu
Course Number and Section: 340-02
Assignment: Aim Rush

Contains logic for how the game functions
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // General Variables for the game to function
    public GameObject[] spawnPoints;
    public GameObject targets;
    public GameObject player;
    public GameObject crosshair;

    public int gamemode;

    public Timer t;

    // Variables for Target Search Mode
    public int targetsSpawned;
    public int targetsRemaining;
    public SpotCheck sc;

    // Variables for Target Wall Mode
    public int targetsDestroyed;
    public bool hasTarget = false;


    // UI Variables
    public Text targetsRemainingText;
    public Text targetsDestroyedText;

    public Text GM1WinTimeRemainingText;
    public Text GM1LossTargetsRemainingText;
    public Text Gm2EndTargetsDestroyedText;

    public GameObject GM1WinScreen;
    public GameObject GM1LossScreen;
    public GameObject GM2EndScreen;

    // Runs checks once per frame
    void Update()
    {
        StartGame();
    }

    // Takes an int input to decide which style of target spawning to do
    public void StartGame()
    {
        if (gamemode == 1)
        {
            FindTargetsMode();
        }

        else if (gamemode == 2)
        {
            AsManyTargetsMode();
        }
    }

    // Updates score based on which gamemode is being played
    public void UpdateScore()
    {
        if (gamemode == 1)
        {
            targetsRemaining--;
            CheckGameOver();
        }

        else if (gamemode == 2)
        {
            hasTarget = false;
            targetsDestroyed++;
            CheckGameOver();
        }
    }

    // Gamemode 1: Target Search
    public void FindTargetsMode()
    {
        RandomTargetSpawn();
        targetsRemainingText.text = targetsRemaining.ToString();
        CheckGameOver();
    }

    // Gamemode 2: Target Wall
    public void AsManyTargetsMode()
    {
        SpawnTargets();
        targetsDestroyedText.text = targetsDestroyed.ToString();
        CheckGameOver();
    }

    // Spawns targets randomly around the map at set spawnpoints. Used in Target Search.
    public void RandomTargetSpawn()
    {
        while (targetsSpawned < targetsRemaining)
        {
            int targetToSpawn = Random.Range(0, spawnPoints.Length);
            if (spawnPoints[targetToSpawn].GetComponent<SpotCheck>().occupiedSpot == false)
            {
                Instantiate(targets, spawnPoints[targetToSpawn].transform.position, spawnPoints[targetToSpawn].transform.rotation);
                spawnPoints[targetToSpawn].GetComponent<SpotCheck>().occupiedSpot = true;
                targetsSpawned++;
            }
        }
    }
 
    // Spawns one taret at a time at a random set spawnpoint. Used in Target Wall
    public void SpawnTargets()
    {
        if (hasTarget == false)
        {
            int targetToSpawn = Random.Range(0, spawnPoints.Length);
            Instantiate(targets, spawnPoints[targetToSpawn].transform.position, Quaternion.identity);
            hasTarget = true;
        }
    }
    
    // Destroys remaining targets in the scene. Used in Target Wall
    public void ResetTargets()
    {
        var remainingTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (var clone in remainingTargets)
            Destroy(clone);
    }

    // Checks for a game end condition
    public void CheckGameOver()
    {
        if (gamemode == 1)
        {
            // Target Search Win
            if (targetsRemaining <= 0)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                player.SetActive(false);
                crosshair.SetActive(false);
                GM1WinScreen.SetActive(true);
                GM1WinTimeRemainingText.text = "Completion Time: " + (30 - t.timeLeft) + " sec";
            }

            // Target Search loss
            else if (t.timeLeft <= 0)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                player.SetActive(false);
                crosshair.SetActive(false);
                GM1LossScreen.SetActive(true);
                GM1LossTargetsRemainingText.text = "Targets Remaining: " + targetsRemaining;
            }
        }

        else if (gamemode == 2)
        {
            // Target Wall timeout
            if (t.timeLeft <= 0)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                ResetTargets();
                player.SetActive(false);
                crosshair.SetActive(false);
                GM2EndScreen.SetActive(true);
                Gm2EndTargetsDestroyedText.text = "Targets Destroyed: " + targetsDestroyed;
            }
        }
    }

    // Makes sure time flows properly when the scene begins...
    private void Awake()
    {
        Time.timeScale = 1f;
    }
}
