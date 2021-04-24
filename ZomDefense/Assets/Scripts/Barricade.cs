using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField]
    private int health = 2000;

    public GameObject canvas;

    public void damageBarricade(int damage)
    {
        health -= damage;
    }

    public void repair()
    {
        health += 50;
        canvas.GetComponent<DaytimeUI>().hoursRemaining -= 3;
    }

    public int getHealth()
    {
        return health;
    }

    public void destroyBarricade()
    {
        this.enabled = false;
    }

    
}
