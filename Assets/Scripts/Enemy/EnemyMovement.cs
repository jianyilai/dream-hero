using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;
    private PlayerMovement player;
    private float moveSpeed;
    private Vector3 directionToPlayer;
    private Vector3 localScale;
    [SerializeField] private float stoppingDistance = 2f;

    private void Awake()
    {
        // Get the Animator component attached to the enemy game object
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Get the RigidBody2D component attached to the enemy game object
        rb = GetComponent<Rigidbody2D>();
        // Find the PlayerMovement component in the scene and assign it to the player variable
        player = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
        // Set the move speed to 2f
        moveSpeed = 2f;
        // Store the local scale of the transform component in the localScale variable
        localScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        // Calculate the distance to the player from the position of the enemy
        float dist = Vector3.Distance(transform.position, player.transform.position);
        // If the calculated distance is less than the assigned stopping distance
        if (dist < stoppingDistance)
        {
            // Call StopEnemy method
            StopEnemy();
        }
        else
        {
            // If calculated distance is still more than stopping distance, MoveEnemy method is called
            MoveEnemy();
        }
    }

    public void MoveEnemy()
    {
        // DirectionToPlayer is the result of the player's position subtract by the posititon of the enemy is normalized
        directionToPlayer = (player.transform.position - transform.position).normalized;
        // Enemy's rigidbody velocity is set to the calculated direction multiply by the assigned move speed
        rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * moveSpeed;
    }

    public void StopEnemy()
    {
        // Set the "canWalk" parameter in the animator component to false
        animator.SetBool("canWalk", false);
        // Set enemy's rigidbody velocity to zero to stop enemy from moving
        rb.velocity = Vector3.zero;
    }

    private void LateUpdate()
    {
        // Check if the x velocity of rigidbody is greater than 0
        if (rb.velocity.x > 0)
        {
            // Set the "canWalk" parameter in the animator component to true
            animator.SetBool("canWalk", true);
            // Change the scale of the enemy object so that it faces to the right
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }
        // If the x velocity of rigidbody is less than 0
        else if (rb.velocity.x < 0)
        {
            // Set the "canWalk" parameter in the animator component to true
            animator.SetBool("canWalk", true);
            // Change the scale of the enemy object so that it faces to the left
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }


}
