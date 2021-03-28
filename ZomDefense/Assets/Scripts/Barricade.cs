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

        if (health <= 0)
            destroyBarricade();
    }

    public void repair()
    {
        health += 100;
    }

    public int getHealth()
    {
        return health;
    }

    private void destroyBarricade()
    {
        GameObject.Destroy(this);
        canvas.GetComponent<uiScript>().gameOver(); // call gameOver() from uiScript to game over
    }

    
}
