using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverScreen : MonoBehaviour
{
    public Healthbar healthbar;
    public Staminabar energybar;
    public Text enemiesLeftText;

    public void Start()
    {
        enemiesLeftText = GetComponent<Text>();
    }
 
    public void Setup(int enemies)
    {
        // Remove healthbar and energybar when the method is called
        healthbar.Die();
        energybar.Die();
        // Set the GameOver Screen game object to active 
        gameObject.SetActive(true);
        // If there are enemies left when player dies
        if (enemies > 1)
        {
            // Set text in GameOverScreen to show number of enemies left
            enemiesLeftText.text = enemies.ToString() + " enemies remaining";
        }
        else
        {
            // Set text in GameOverScreen to show enemy left if there is only one enemy remaining
            enemiesLeftText.text = enemies.ToString() + " enemy remaining";
        }
    }

    public void RestartButton()
    {
        // Reload current scene to restart the level
        SceneManager.LoadScene("Level1Scene");
    }

    public void ExitButton() 
    {
        // Load the Main Menu Scene
        SceneManager.LoadScene("Menu");
    }
}
