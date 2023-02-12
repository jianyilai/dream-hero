using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public Transform attackPointLeft;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 25;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Update()
    {
        // Check if it is time for the next attack
        if (Time.time >= nextAttackTime)
        {
            // Check if the space key is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Check if player is facing left or right
                if (animator.GetBool("isFacingRight"))
                {
                    // Play right attack animation
                    animator.SetTrigger("BasicAttack");
                    Attack();
                    // Set the time for the next attack
                    nextAttackTime = Time.time + 1f / attackRate;
                }
                else
                {
                    // Play left attack animation
                    animator.SetTrigger("BasicAttack");
                    LeftAttack();
                    // Set the time for the next attack
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }

    }

    // Method for the attack behavior of an object
    void Attack()
    {
        // Use Physics2D.OverlapCircleAll to detect all colliders on the enemyLayers in a circle centered at attackPoint.position with a radius of attackRange
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Iterate over all colliders found in the circle
        foreach (BoxCollider2D enemy in hitEnemies)
        {
            // Call the TakeDamage method inside the EnemyBehaviour component of each collider to apply damage
            enemy.GetComponent<EnemyBehaviour>().TakeDamage(attackDamage);
        }
    }

    // Method for the attack behavior of an object but facing left
    void LeftAttack()
    {
        // Use Physics2D.OverlapCircleAll to detect all colliders on the enemyLayers in a circle centered at attackPoint.position with a radius of attackRange
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointLeft.position, attackRange, enemyLayers);

        // Iterate over all colliders found in the circle
        foreach (BoxCollider2D enemy in hitEnemies)
        {
            // Call the TakeDamage method inside the EnemyBehaviour component of each collider to apply damage
            enemy.GetComponent<EnemyBehaviour>().TakeDamage(attackDamage);
        }
    }

}
