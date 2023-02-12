using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public Healthbar healthbar;
    // To make the enemy attack right away
    private float coolddownTimer = Mathf.Infinity;
    private PlayerBehaviour playerHealth;
    public GameObject itemDrops;

    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    // Declare a public static int variable named "remainingEnemies" with a default value of 0
    public static int remainingEnemies = 0;

    private void Awake()
    {
        // Get the Animator component attached to the enemy game object
        animator = GetComponent<Animator>();
        // Increment the value of the static variable remainingEnemies, which keeps track of the number of remaining enemies in the game
        remainingEnemies++;
    }

    void Start()
    {
        // Set the currentHealth value to the maxHealth value
        currentHealth = maxHealth;
        // Set the healthbar to current health
        healthbar.SetMaxHealth(maxHealth);

        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Increment the cooldownTimer by Time.deltaTime on every frame
        coolddownTimer += Time.deltaTime;

        //Attack only when player in sight
        if (PlayerInSight())
        {
            if (coolddownTimer >= attackCooldown)
            {
                // Attack
                // Reset cooldown timer to 0
                coolddownTimer = 0;
                // Trigger attack animation
                animator.SetTrigger("Attack");
            }
        }
    }

    // Function to check if player is within sight range
    private bool PlayerInSight()
    {
        // Cast a boxCollider to detect if any game object in the player layer is inside the box
        RaycastHit2D hit = Physics2D.BoxCast(
            // Origin of the boxCollider plus the range multiplied by localScale.x (Flip the collider depending on the direction enemy is facing) and the distance of the collider
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            // Size of the boxCollider
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            // Angle
            0,
            // Direction
            Vector2.left,
            // Distance
            0,
            // Detects player only on playerLayer
            playerLayer
        );

        // If the boxCollider detects a player, store the player's health script in playerHealth variable
        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<PlayerBehaviour>();

        // Return true or false whether the boxCollider detects a player or not
        return hit.collider != null;
    }

    // Function to display the collider range in the scene view
    private void OnDrawGizmos()
    {
        // Set the color of the gizmo to red
        Gizmos.color = Color.red;
        // Draw a wire cube for the boxCollider casted in PlayerInSight function for visualization
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // This method is called as an animation event when enemy uses the attack animation
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            // If player is in range, call TakeDamage method
            playerHealth.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        // Subtract enemy's health with player's damage
        currentHealth -= damage;
        // Set the healthbar to current health
        healthbar.SetHealth(currentHealth);

        // Play hurt animation
        animator.SetTrigger("Hurt");

        // If enemy health is 0, then call Die method and ItemDrop method
        if (currentHealth <= 0)
        {
            Die();
            ItemDrop();
        }
    }

    void Die()
    {
        // Stops enemy movement during death animation
        // Disable the physics behaviour of the game object
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        // Freeze the position of the game object on both the X and Y axis
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

        // Death animation
        animator.SetBool("isDead", true);

        // Disable the enemy
        GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;

        // Destroy enemy sprite after animation
        Destroy(gameObject, 1);
    }

    private void OnDestroy()
    {
        // Subtract 1 from remainingEnemies when the enemy dies
        remainingEnemies--;
    }

    private void ItemDrop()
    {
        int randomNumber = Random.Range(1, 101); // Generate a number from 1 to 100

        if (randomNumber >= 70) // 30% chance for random number to be 70 or above
        {
            // Spawn item on the enemy's position with no rotation
            Instantiate(itemDrops, transform.position, Quaternion.identity);
        }
    }

}
