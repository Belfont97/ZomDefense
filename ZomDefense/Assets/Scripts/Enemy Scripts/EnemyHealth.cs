using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    EnemyMovement movement;
    CapsuleCollider enemyCollider;

    private void Start()
    {
        movement = gameObject.GetComponent<EnemyMovement>();
        enemyCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        movement.animator.SetBool("isDead", true);
        movement.animator.SetBool("isMoving", false);

        gameObject.tag = "Dead Zombie";
        enemyCollider.enabled = false;
    }
}
