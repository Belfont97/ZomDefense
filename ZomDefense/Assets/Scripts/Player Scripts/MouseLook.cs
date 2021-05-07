using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f; // sensitvity of looking

    public Transform playerBody; // reference to the player's body

    public float xRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks the cursor when playing
    }

    // Update is called once per frame
    void Update()
    {
        // get the mouse x-axis input, multiply by mouseSensitivity to control sensitivity, multiply by Time.deltaTime to move camera independant of frame rate (high framerate doesn't mean faster turn, slow framerate doesn't mean slow turn)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // same as above for y-axis

        xRotation -= mouseY; // decrease xrotation by mousey every frame (increasing flips rotation (bad))
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // prevents xRotation from exceeding 90 degrees each direction; player can't break their neck looking behind them

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // apply up/down rotation (Quaternion: rotation in unity, euler angles take x, y, z rotation)
        playerBody.Rotate(Vector3.up * mouseX); // rotates the player side to side by rotating around the y-axis
    }
}
