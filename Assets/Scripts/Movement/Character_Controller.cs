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

    //recoil
    public float xOffset { get; set; }
    public float yOffset { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        charCollider = GetComponent<CapsuleCollider>();

        speed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;

        x = 0;
        y = 0;
        xOffset = 0;
        yOffset = 0;
    }

 
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = mainCamera.transform.forward * vertical + mainCamera.transform.right * horizontal;

        moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);
        moveDirection = Vector3.Normalize(moveDirection);

        transform.Translate(moveDirection * speed * Time.deltaTime);
        playerBody.Rotate(Vector3.up * horizontal);

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
}

