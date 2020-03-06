/*
Name: Nick Lai
Student ID#: 2282417
Chapman email: lai137@mail.chapman.edu
Course Number and Section: 340-02
Assignment: Aim Rush

Reenables the cursor when entering the Main Menu again after using the FPSController
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
