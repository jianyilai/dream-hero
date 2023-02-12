using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    int dashForce = 400000;

    public Staminabar staminaBar;
    public int maxStamina = 100;
    public float currentStamina;
    public float staminaRecoveryRate;

    void Start()
    {
        // Set current stamina to the max stamina value which is 100
        currentStamina = maxStamina;
        // Set the staminabar to current stamina
        staminaBar.SetStartingStamina(currentStamina);
    }

    void Update()
    {
        // Get player movement input from the horizontal and vertical axis
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Check if player is moving left
        if (movement.x < 0f)
        {   
            isFacingLeft();
        }
        else
        {
            isFacingRight();
        }

        // Set animator parameters based on player movement
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Check if player presses the dash button
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Check if player has enough stamina to dash
            if (currentStamina >= 30)
            {
                // Reduce player stamina by 30
                currentStamina -= 30;
                staminaBar.SetStamina(currentStamina);
                // Perform the dash
                Dash();
                
            }
        }
        else
        {
            // Restore player stamina
            RecoverStamina();
        }
    }

    void FixedUpdate()
    {
        // Move player's Rigidbody by applying movement force multiplied by move speed and fixed delta time to simulate smooth movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void isFacingLeft()
    {
        // Check if player is moving left
        if (movement.x < 0f)
        {
            // Set animator parameters to play walk left animation
            animator.SetBool("isFacingLeft", true);
            animator.SetBool("isFacingRight", false);
        }
    }

    private void isFacingRight()
    {
        // Check if player is moving right
        if (movement.x > 0f)
        {
            // Set animator parameters to play walk right animation
            animator.SetBool("isFacingRight", true);
            animator.SetBool("isFacingLeft", false);
        }
    }

    private void Dash()
    {
        // Apply dash force in the direction of player input by normalizing the input and adding force to the Rigidbody
        Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        // Dash direction is multiplied by dash force and fixed delta time to simulate smooth movement
        rb.AddForce(dashDirection * dashForce * Time.fixedDeltaTime);
    }

    private void RecoverStamina()
    {
        // Check if current stamina is lesser than max stamina
        if (currentStamina < maxStamina)
        {
            // Add a recover stamina amount to the player's current stamina multiply by delta time for smooth visualization on the stamina bar
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            // Check if current stamina is more than max stamina
            if(currentStamina > maxStamina)
            {
                // Set current stamina equal to max stamina to avoid current stamina from going above max stamina
                currentStamina = maxStamina;
            }
            // Set the staminabar to current stamina
            staminaBar.SetStamina(currentStamina);
        }
    }
}
