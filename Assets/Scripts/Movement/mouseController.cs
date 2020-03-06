using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseController : MonoBehaviour
{
    //variables for camera
    private CharacterController cc;
    public Camera mainCamera;

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
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();
        //mainCamera = GetComponent<Camera>();
        x = 0;
        y = 0;
        xOffset = 0;
        yOffset = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}

