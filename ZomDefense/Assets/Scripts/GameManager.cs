﻿using System.Collections;
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

    private int enemiesToSpawn;
    private bool enemiesSpawned;
    private bool dayUIShown = false;

    private static State gameState = State.NIGHT;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        enemiesSpawned = false;
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
                        case 1: enemiesToSpawn = 5;
                            break;

                        case 2: enemiesToSpawn = 5;
                            break;

                        case 3: enemiesToSpawn = 5;
                            break;

                        default: enemiesToSpawn = 1;
                            break;
                    }

                    if (!enemiesSpawned) // if enemies haven't been spawned this night, spawn enemies
                    {
                        Debug.Log("Spawning enemies");
                        enemySpawner.GetComponent<EnemySpawner>().spawnEnemy(enemiesToSpawn);
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
                        gameState = State.DAY;
                    }

                    break;

                case State.DAY:
                    Debug.Log("Day state reached");   
                    
                    if (!dayUIShown)
                    {
                        canvas.GetComponent<DaytimeUI>().showDayUI();
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
}
