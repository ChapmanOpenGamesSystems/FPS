/*
Name: Nick Lai
Student ID#: 2282417
Chapman email: lai137@mail.chapman.edu
Course Number and Section: 340-02
Assignment: Aim Rush

Contains logic for the Targets used in both gamemodes
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    public float health = 1f;
    private GameManager gm;
    private SoundManager sm;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gm.UpdateScore();
            sm.PlaySound(sm.targetBreak, 0.25f);
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }
}
