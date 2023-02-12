using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScreen : MonoBehaviour
{
    public Healthbar healthbar;
    public Staminabar energybar;
    public void Setup()
    {
        // Remove healthbar and energybar when the method is called
        healthbar.Die();
        energybar.Die();
        // Set the Level Complete Screen game object to active 
        gameObject.SetActive(true);
    }

    public void ReplayButton()
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
