using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Controls the movement of the player camera and the player model for the first person camera
public class PlayerLook : MonoBehaviour
{
    public float        mouseSensitivity    = 100f;
    private Transform    playerBody;

    float               xRotation           = 0f;


    // Start is called before the first frame update
    // Locks the mouse onto the application
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = GameObject.Find("First Person Player").GetComponent<Transform>();
    }


    // Update is called once per frame
    // Looks at the input from the mouse in order to calculate where the camera and player should face
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamps so the player can't look past the top or bottom of the screen

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotates the camera vertically
        playerBody.Rotate(Vector3.up * mouseX); // Rotates the player horizontally and thus the camera
    }
}