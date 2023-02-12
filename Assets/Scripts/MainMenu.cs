using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Menu()
    {
        // Load the Main Menu Scene
        SceneManager.LoadScene("Menu");
    }

    public void Credits()
    {
        // Load the Credits Scene
        SceneManager.LoadScene("Credits");
    }

    public void PlayGame()
    {
        // Load the Level1Scene Scene
        SceneManager.LoadScene("Level1Scene");
    }
    
    public void QuitGame()
    {
        // Quit application
        Application.Quit();
    }
}
