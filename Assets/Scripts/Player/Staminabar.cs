using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Staminabar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // Method to set the maximum stamina of a game object and display it on a slider
    public void SetStartingStamina(float stamina)
    {
        // Set the maximum value of the slider component to the stamina value passed in as parameter
        slider.maxValue = 100;
        // Set the current value of the slider component to the stamina value passed in as parameter
        slider.value = stamina;
        // set gradient color to blue
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Method to set the current stamina of a game object and display it on a slider
    public void SetStamina(float stamina)
    {
        // Set the current value of the slider component to the stamina value passed in as parameter
        slider.value = stamina;
        // Set the gradient color to a color evaluated from the gradient using the normalized value of the slider
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // This method is called when GameOver screen or LevelComplete screen is active
    public void Die()
    {
        // Set the "energybar" game object to not active
        gameObject.SetActive(false);
    }
}
