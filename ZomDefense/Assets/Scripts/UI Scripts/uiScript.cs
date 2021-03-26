using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class uiScript : MonoBehaviour
{
    public Text healthText;
    public GameObject barricade;
    public GameObject player;
    public GameObject playerCamera;

    public Text ammoText;
    public GameObject gunObject;

    public Text gameOverText;

    private void Start()
    {
        gameOverText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Barricade Health: " + barricade.GetComponent<Barricade>().getHealth().ToString(); // display health
        ammoText.text = "Ammo: " + gunObject.GetComponent<Gun>().getCurrentAmmo().ToString(); // display ammo
    }

    

    public void gameOver()
    {
        gameOverText.enabled = true;
        player.GetComponent<PlayerMovement>().enabled = false; // disable player movement
        playerCamera.GetComponent<MouseLook>().enabled = false; // disable camera movement from mouse
        gunObject.GetComponent<Gun>().enabled = false; // disable gun mechanics
        Cursor.lockState = CursorLockMode.Confined; // unlock the cursor to click again
    }
}
