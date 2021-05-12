using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject gameManager;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Game Manager") == null)
        {
            GameObject.Instantiate(gameManager);
            Debug.Log("Game Manager Instantiated");
        }
        else
        {
            gameManager = GameObject.FindGameObjectWithTag("Game Manager");
            gameManager.GetComponent<GameManager>().resetGame();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("ZomDefense");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
