using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public Transform spawnPosition;

    public void spawnEnemy(int enemyNum)
    {
        for (int i = 0; i < enemyNum; i++)
            Instantiate(enemyToSpawn, spawnPosition.position, Quaternion.identity, spawnPosition);
    }
}
