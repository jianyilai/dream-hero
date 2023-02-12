using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFood : MonoBehaviour
{
    public GameObject player;
    public int foodHeal = 20;

    // Check for collisions with other colliders in the scene
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collider that entered the trigger is tagged as "Player"
        if (collision.tag == "Player")
        {
            // Check if currentHealth of Player is less than 100
            if (GameObject.Find("Rose").GetComponent<PlayerBehaviour>().currentHealth < 100)
            {
                // Call Heal method inside PlayerBehaviour component to add the assigned foodHeal value to the currentHealth
                GameObject.Find("Rose").GetComponent<PlayerBehaviour>().Heal(foodHeal);
                // Destroy the "food" game object after player touches it
                Destroy(gameObject);
            }
        }
    }
}
