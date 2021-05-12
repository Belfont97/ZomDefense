using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int nightNumber = 1;
    public int dayNumber = 1;

    private GameObject enemySpawner;
    private GameObject canvas;
    private GameObject barricade;

    [SerializeField]
    private int normalZomToSpawn = 0;
    [SerializeField]
    private int fastZomToSpawn = 0;
    [SerializeField]
    private bool enemiesSpawned;
    [SerializeField]
    private bool dayUIShown = false;

    private static State gameState = State.NIGHT;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update() 
    {
        if (SceneManager.GetActiveScene().name == "ZomDefense")
        {
            enemySpawner = GameObject.FindGameObjectWithTag("Enemy Spawner");
            canvas       = GameObject.FindGameObjectWithTag("UI");
            barricade    = GameObject.FindGameObjectWithTag("Barricade");

            switch (gameState)
            {
                case State.NIGHT:
                    switch(nightNumber) // Depending on which night, spawn different amounts of enemies, more as the game progresses
                    {
                        case 1: normalZomToSpawn = 5;
                            fastZomToSpawn = 2;
                            break;

                        case 2: normalZomToSpawn = 10;
                            fastZomToSpawn = 5;
                            break;

                        case 3: normalZomToSpawn = 15;
                            fastZomToSpawn = 10;
                            break;

                        default: normalZomToSpawn = 25;
                            fastZomToSpawn = 15;
                            break;
                    }

                    if (!enemiesSpawned) // if enemies haven't been spawned this night, spawn enemies
                    {
                        Debug.Log("Spawning enemies");
                        enemySpawner.GetComponent<EnemySpawner>().spawnZoms(normalZomToSpawn, fastZomToSpawn);
                        enemiesSpawned = true;
                    }

                    if (barricade.GetComponent<Barricade>().getHealth() <= 0) //if barricade is destroyed, move to DESTROYBARRICADE state
                    {
                        gameState = State.DESTROYBARRICADE;
                        Debug.Log("Destroying barricade");
                        enemiesSpawned = false;
                    }

                    if (GameObject.FindGameObjectsWithTag("Zombie").Length == 0) // if no more zombies, move to NIGHTOVER state
                    {
                        Debug.Log("All zombies eliminated");
                        gameState = State.NIGHTOVER;
                        enemiesSpawned = false;
                    }

                    break;

                case State.DESTROYBARRICADE:
                    barricade.GetComponent<Barricade>().destroyBarricade();
                    Debug.Log("Barricade destroyed, ending game");
                    gameState = State.GAMELOSE;
                    break;

                case State.NIGHTOVER:
                    Debug.Log("Night ended");
                    if (nightNumber > 2) // if night three has ended, move to GAMEWIN state
                        gameState = State.GAMEWIN;
                    else
                    {
                        nightNumber++;
                        cleanUp();
                        gameState = State.DAY;
                    }

                    break;

                case State.DAY:
                    if (!dayUIShown)
                    {
                        canvas.GetComponent<DaytimeUI>().showDayUI();
                        Debug.Log("Day state reached");
                        dayUIShown = true;
                    }
                        
                    if (!canvas.GetComponent<DaytimeUI>().isActive)
                    {
                        gameState = State.NIGHT;
                        dayNumber++;
                        dayUIShown = false;
                        Debug.Log("Moving to next night");
                    }
                    
                    break;

                case State.GAMEWIN:
                    Debug.Log("Game won!");
                    canvas.GetComponent<uiScript>().gameWin();
                    break;

                case State.GAMELOSE:
                    Debug.Log("Game lost, starting game over");
                    canvas.GetComponent<uiScript>().gameOver(); // call gameOver() from uiScript to game over
                    break;

                default:
                    Debug.Log("Unkown game state!");
                    break;
            }
        }
    }

    /**
     * Cleanup game area by destroying dead zombies
     */
    private void cleanUp()
    {
        GameObject[] deadZombies = GameObject.FindGameObjectsWithTag("Dead Zombie");

        foreach (GameObject zombie in deadZombies)
        {
            Destroy(zombie);
        }
    }

    public enum State
    {
        DEFAULT,
        SPAWNENEMIES,
        DAY,
        NIGHT,
        DESTROYBARRICADE,
        GAMELOSE,
        GAMEWIN,
        NIGHTOVER
    };

    public void resetGame()
    {
        normalZomToSpawn = 0;
        fastZomToSpawn = 0;
        enemiesSpawned = false;
        nightNumber = 1;
        dayNumber = 1;

        Debug.Log("Game Manager Reset");
    }
}
