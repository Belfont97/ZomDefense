using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject NormalZom;
    public GameObject FastZom;

    public Transform spawnPosition;

    public void spawnNormalZom(int enemyNum)
    {
        for (int i = 0; i < enemyNum; i++)
            Instantiate(NormalZom, spawnPosition.position, Quaternion.identity, spawnPosition);
    }

    public void spawnFastZom(int enemyNum)
    {
        for (int i = 0; i < enemyNum; i++)
            Instantiate(FastZom, spawnPosition.position, Quaternion.identity, spawnPosition);
    }
}
