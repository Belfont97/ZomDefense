using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackSpeed = 1.2f;
    public float attackRange = 3.8f;

    EnemyMovement attacker;
    
    public IEnumerator Attack()
    {
        attacker = gameObject.GetComponent<EnemyMovement>();

        attacker.isAttacking = true; // prevent more coroutines from running while this one executes

        attacker.animator.SetBool("isMoving", false);
        attacker.animator.SetBool("isAttacking", true);

        attacker.getTarget().GetComponent<Barricade>().damageBarricade(damage); // hurt player for amount of damage

        yield return new WaitForSeconds(attackSpeed); // wait seconds before allowing another coroutine to execute

        attacker.isAttacking = false; // end coroutine, allow another coroutine to execute
    }
}
