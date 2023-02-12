using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;
    public Healthbar healthbar;
    public GameOverScreen GameOverScreen;
    public LevelCompleteScreen LevelCompleteScreen;
    public TextMeshProUGUI enemiesCounterText;

    void Start()
    {
        // Set the currentHealth value to the maxHealth value
        currentHealth = maxHealth;
        // Set the healthbar to current health
        healthbar.SetMaxHealth(currentHealth);
    }

    void Update()
    {
        // Update the enemies counter text every frame
        enemiesCounterText.text = "Enemies Left: " + EnemyBehaviour.remainingEnemies;

        // Check if there are no remaining enemies
        if (EnemyBehaviour.remainingEnemies == 0)
        {
            // Call the Setup method of the LevelCompleteScreen object to display Level Complete
            LevelCompleteScreen.Setup();
        }
    }

    public void TakeDamage(int damage)
    {
        // Subtract player's health with enemy's damage
        currentHealth -= damage;
        // Set the healthbar to current health
        healthbar.SetHealth(currentHealth);

        // If player's health reaches 0
        if (currentHealth <= 0)
        {
            // Set the player game object to not active
            gameObject.SetActive(false);
            // Call the Setup method of the GameOverScreen object to display Game Over
            GameOverScreen.Setup(EnemyBehaviour.remainingEnemies);
        }
    }


    public void Heal(int heal)
    {
        // Add a heal amount to player's current health
        currentHealth += heal;
        // Check if current health is more than max health
        if (currentHealth > maxHealth) 
        { 
            // Set current health equal to max health to avoid current health from going above max health
            currentHealth = maxHealth; 
        }
        // Set the healthbar to current health
        healthbar.SetHealth(currentHealth);
    }
}
