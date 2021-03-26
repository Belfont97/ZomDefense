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
    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void Die()
    {
        movement.animator.SetBool("isDead", true);
        movement.animator.SetBool("isMoving", false);
        
        enemyCollider.enabled = false;
    }
}
