/*
Name: Nick Lai
Student ID#: 2282417
Chapman email: lai137@mail.chapman.edu
Course Number and Section: 340-02
Assignment: Aim Rush

Contains logic for exiting the game. Used on a button
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    // Quits the game
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit the game");
    }
}
