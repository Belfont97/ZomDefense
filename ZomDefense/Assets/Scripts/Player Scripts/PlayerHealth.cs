using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public GameObject canvas;
    public GameObject player;
    public GameObject playerCamera;
    public GameObject Gun;
    public void killPlayer()
    {
        if (health <= 100)
        {
            canvas.GetComponent<uiScript>().gameOver(); // call gameOver() from uiScript to display game over text
            player.GetComponent<PlayerMovement>().enabled = false; // disable player movement
            playerCamera.GetComponent<MouseLook>().enabled = false; // disable camera movement from mouse
            Gun.GetComponent<Gun>().enabled = false; // disable gun mechanics
            Cursor.lockState = CursorLockMode.Confined; // unlock the cursor to click again
        }
    }
}
