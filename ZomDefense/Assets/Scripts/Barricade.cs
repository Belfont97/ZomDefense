using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    [SerializeField]
    private int health = 300;
    private int maxHealth = 300;

    public GameObject canvas;

    public void damageBarricade(int damage)
    {
        health -= damage;
    }

    public void repair()
    {
        health += 50;

        if (health > maxHealth)
            health = maxHealth;

        canvas.GetComponent<DaytimeUI>().hoursRemaining -= 3;
    }

    public int getMaxHealth()
    {
        return maxHealth;

    }    
    public int getHealth()
    {
        return health;
    }

    public void setMaxHealth(int newMaxHealth)
    {
        maxHealth += newMaxHealth;
        canvas.GetComponent<DaytimeUI>().hoursRemaining -= 5;
    }

    public void destroyBarricade()
    {
        this.enabled = false;
    }

    
}
