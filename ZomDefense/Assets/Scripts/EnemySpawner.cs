using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject NormalZom;
    public GameObject FastZom;

    public Transform spawnPosition;

    public void spawnZoms(int normZoms, int fastZoms)
    {
        for (int i = 0; i < normZoms; i++)
            Instantiate(NormalZom, spawnPosition.position, Quaternion.identity, spawnPosition);

        for (int i = 0; i < fastZoms; i++)
            Instantiate(FastZom, spawnPosition.position, Quaternion.identity, spawnPosition);
    }
}
