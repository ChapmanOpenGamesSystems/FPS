/*
Name: Nick Lai
Student ID#: 2282417
Chapman email: lai137@mail.chapman.edu
Course Number and Section: 340-02
Assignment: Aim Rush

Contains logic for FPS Movement (Lean and Crouch)
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    // Variables for Leaning
    public Transform leanPivot;

    Coroutine isLeaning;
    public bool isLeaningLeft = false;
    public bool isLeaningRight = false;

    // Variables for stance change
    private CharacterController cc;

    public bool isCrouched = false;
    public float standingHeight;
    [SerializeField] public float crouchHeight = 0.5f;

    // Gets lean pivot on Awake
    void Awake()
    {
        if (leanPivot == null && transform.parent != null) leanPivot = transform.parent;
        cc = GetComponentInParent<CharacterController>();
        standingHeight = cc.height;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLean();
        UpdateStance();
    }

    // Contains logic for updating player stance (Crouch and Standing
    public void UpdateStance()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(!isCrouched)
            {
                cc.height = crouchHeight;
                isCrouched = true;
            }

            else if (isCrouched)
            {
                cc.height = standingHeight;
                isCrouched = false;
            }
        }
    }

    // Contains logic for updating Camera lean angles.
    void UpdateLean()
    {
        // Leans left
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(isLeaningLeft) {
                isLeaningLeft = false;
                StopCoroutine(isLeaning);
                isLeaning = StartCoroutine(RotateSmooth(Vector3.forward, 0, 0.2f));
            }
            else
            {
                if (isLeaningRight)
                {
                    isLeaningRight = false;
                    StopCoroutine(isLeaning);
                }
                //Lean 15 degrees when leaning from center
                isLeaning = StartCoroutine(RotateSmooth(Vector3.forward, 15, 0.2f));
                isLeaningLeft = true;
            }

        }

        // Leans right
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (isLeaningRight)
            {
                isLeaningRight = false;
                StopCoroutine(isLeaning);
                isLeaning = StartCoroutine(RotateSmooth(Vector3.forward, 0, 0.2f));
            }
            else
            {
                if (isLeaningLeft)
                {
                    isLeaningLeft = false;
                    StopCoroutine(isLeaning);
                }
                //Lean 15 degrees when leaning from center
                isLeaning = StartCoroutine(RotateSmooth(Vector3.forward, -15, 0.2f));
                isLeaningRight = true;
            }
        }
    }

    internal static void RotateSmooth()
    {
        throw new NotImplementedException();
    }

    // Used to rotate camera based on angles and time duration
    IEnumerator RotateSmooth(Vector3 axis, float angle, float duration)
    {
        Quaternion from = transform.localRotation;
        Quaternion to = transform.localRotation;

        to = Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while(elapsed < duration)
        {
            transform.localRotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localRotation = to;
    }

}
