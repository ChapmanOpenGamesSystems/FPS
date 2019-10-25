/*
Name: Nick Lai
Student ID#: 2282417
Chapman email: lai137@mail.chapman.edu
Course Number and Section: 340-02
Assignment: Aim Rush

Contains logic for playing OneShots and stores Audio Clips
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource aus;

    public AudioClip gunfire;
    public AudioClip reload;
    public AudioClip targetBreak;
    public AudioClip buttonClick;

    // Start is called before the first frame update
    void Start()
    {
        aus = GetComponent<AudioSource>();
    }

    // Plays an audio clip, given the clip and a volume level
    public void PlaySound(AudioClip clip, float volume)
    {
        aus.PlayOneShot(clip, volume);
    }
}
