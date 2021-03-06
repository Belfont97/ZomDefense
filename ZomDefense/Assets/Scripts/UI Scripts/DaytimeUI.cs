using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaytimeUI : MonoBehaviour
{
    public GameObject barricade;
    public GameObject daytimeUI;

    public GameObject player;
    public GameObject playerCamera;
    public GameObject gunObject;

    public Text hoursRemainingText;
    public int hoursRemaining;

    public Text barricadeHealth;

    public Button repairBarricade;
    public Button reinforceBarricade;
    public Button fixLightsButton;
    public Button scavengeBullets;

    private GameObject[] lights;

    public bool isActive;
    public bool lightsOn = false;

    private void Start()
    {
        daytimeUI.SetActive(false);
        hoursRemaining = 8;
        lights = GameObject.FindGameObjectsWithTag("Street Light");
    }

    private void Update()
    {
        // Enable or disable repair barricade button
        if (barricade.GetComponent<Barricade>().getHealth() >= barricade.GetComponent<Barricade>().getMaxHealth() || hoursRemaining < 3)
            repairBarricade.enabled = false;         
        else
            repairBarricade.enabled = true;

        // Enable or disable fix lights button
        if (lightsOn == false && hoursRemaining >= 5)
            fixLightsButton.enabled = true;
        else
            fixLightsButton.enabled = false;

        // Enable or disable reinforce barricade button
        if (hoursRemaining < 5)
            reinforceBarricade.enabled = false;
        else
            reinforceBarricade.enabled = true;

        // Enable or disable scavenge bullets button
        if (hoursRemaining < 3)
            scavengeBullets.enabled = false;
        else
            scavengeBullets.enabled = true;

        hoursRemainingText.text = "Hours Remaining: " + hoursRemaining; // set hours remaining text
        barricadeHealth.text = "Barricade Health: " + barricade.GetComponent<Barricade>().getHealth() + "/" + barricade.GetComponent<Barricade>().getMaxHealth(); // set barricade health text
    }

    public void showDayUI()
    {
        hoursRemaining = 8;
        isActive = true;
        Cursor.lockState = CursorLockMode.Confined; // show cursor, keep within screen
        player.GetComponent<PlayerMovement>().enabled = false; // disable player movement
        playerCamera.GetComponent<MouseLook>().enabled = false; // disable camera movement from mouse
        gunObject.GetComponent<Gun>().enabled = false; // disable gun mechanics
        daytimeUI.SetActive(true);
    }

    public void nextNight()
    {
        Debug.Log("nextNight() called");
        daytimeUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<PlayerMovement>().enabled = true; // enable player movement
        playerCamera.GetComponent<MouseLook>().enabled = true; // enable camera movement from mouse
        gunObject.GetComponent<Gun>().enabled = true; // enable gun mechanics
        isActive = false;
        Debug.Log("Reached end of nextNight()");
    }

    public void fixLights()
    {
        lights = GameObject.FindGameObjectsWithTag("Street Light");

        Debug.Log(lights.Length);

        foreach (GameObject light in lights)
            light.GetComponent<Light>().enabled = true;

        lightsOn = true;

        hoursRemaining -= 5;
    }
}
