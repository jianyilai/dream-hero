using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // Method to set the maximum health of a game object and display it on a slider
    public void SetMaxHealth(int health)
    {
        // Set the maximum value of the slider component to the health value passed in as parameter
        slider.maxValue = health;
        // Set the current value of the slider component to the health value passed in as parameter
        slider.value = health;
        // Set gradient color to green
        fill.color = gradient.Evaluate(1f);
    }

    // Method to set the current health of a game object and display it on a slider
    public void SetHealth(int health)
    {
        // Set the current value of the slider component to the health value passed in as parameter
        slider.value = health;
        // Set the gradient color to a color evaluated from the gradient using the normalized value of the slider
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // This method is called when GameOver screen or LevelComplete screen is active
    public void Die()
    {
        // Set the "healthbar" game object to not active
        gameObject.SetActive(false);
    }
}
