using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject enemy;

    private void Start()
    {
        // At the start of the game, set the "enemy" game object to not active
        enemy.SetActive(false);
    }

    // Check for collisions with other colliders in the scene
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collider that entered the trigger is tagged as "Player", set the "enemy" game object to active
        if (collision.tag == "Player")
        {
            enemy.SetActive(true);
        }
    }
}
