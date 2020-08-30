//Edited by Scott Cummings Aug-29-2020
//Player now follows the direction of the camera when pressing forward and there is no vertical movement when only pressing
//keys for the horizontal.
//Next Steps: Fix Movement for colliding with objects
//            Fix gun movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{

    //character variables
    Rigidbody myBody;
    public Transform playerBody;

    //movement variables
    private float speed;
    public float walkSpeed;
    public float sprintSpeed;

    //jump variables
    [Range(1, 10)]
    float jumpVelocity;

    //cleanJump
    float fallMultiplier = 2.0f;
    float lowJumpMultiplier = 2.5f;


    //bools for different character states
    bool isGrounded;
    bool canJump;

    //variables for camera
    public Camera mainCamera;
    CapsuleCollider charCollider;

    //variables for camera movement
    public float sensitivity = 2.0f;
    float x;
    float y;
    float distance = 2; 

    //recoil
    public float xOffset { get; set; }
    public float yOffset { get; set; }

    private void Awake()
    {

        GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
        myBody = GetComponent<Rigidbody>();
        charCollider = GetComponent<CapsuleCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked; //Ensure that the camera is locked to the center of the screen

        x = 0;
        y = 0;
        xOffset = 0;
        yOffset = 0;
    }

 
    // Update is called once per frame
    void Update()
    {
        //get the inputs from keyboard
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Changed from:
        //Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal; 
        Vector3 moveDirection = Camera.main.transform.forward * vertical + Camera.main.transform.right * horizontal;

        moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);
        moveDirection = Vector3.Normalize(moveDirection);

        transform.Translate(moveDirection * speed * Time.deltaTime);

        //The following line breaks the script
        //playerBody.Rotate(Vector3.up * horizontal)


        //mouse look 
        if (mainCamera.enabled)
        {
            x += sensitivity * Input.GetAxis("Mouse X") + xOffset;
            y += sensitivity * Input.GetAxis("Mouse Y") + yOffset;
            if (y >= 90)
            {
                y = 90;
            }
            else if (y <= -90)
            {
                y = -90;
            }
            mainCamera.transform.eulerAngles = new Vector3(-y, x, 0);
            xOffset = 0;
            yOffset = 0;
        }
        
        Sprint();
        Jump();
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = walkSpeed;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CleanJump();
        }
    }

    void CleanJump()
    {
        if (myBody.velocity.y < 0)
        {
            myBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (myBody.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            myBody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}

