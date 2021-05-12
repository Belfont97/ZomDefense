using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameisPaused = false;

    public GameObject pauseMenuUI;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked; // hide away cursor
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.Confined; // show cursor, keep within screen
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
