using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int nightNumber = 1;
    public int dayNumber = 1;

    private GameObject player;
    private GameObject enemySpawner;
    private GameObject canvas;
    private GameObject barricade;

    private int enemiesToSpawn;
    private int enemiesKilled;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        enemySpawner = GameObject.FindGameObjectWithTag("Enemy Spawner");

        canvas = GameObject.FindGameObjectWithTag("UI");

        barricade = GameObject.FindGameObjectWithTag("Barricade");
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case State.SPAWNENEMIES:
                enemiesToSpawn = 10;

                for (int i = 0; i < enemiesToSpawn; i++)
                    enemySpawner.GetComponent<EnemySpawner>().spawnEnemy();

                if (enemiesKilled == 10)
                    canvas.GetComponent<uiScript>().gameOver();
                break;

            default:
                break;
        }
    }

    public enum State()
    {
        DEFAULT,
        SPAWNENEMIES,
        DAY,
        NIGHT,
        DESTROYBARRICADE,
        GAMELOSE,
        GAMEWIN,
        DAYOVER,
        NIGHTOVER
    };
}
