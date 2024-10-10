using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f; // Set initial health for the player
    public Image healthBarFill; // Reference to the health bar UI's Image component

    public float CurrentHealth => health;

    void Start()
    {
        // Optional: If the health bar is not set in the Inspector, try to find it
        if (healthBarFill == null)
        {
            healthBarFill = GameObject.Find("HealthBarFill").GetComponent<Image>();
        }

        // Initialize health bar
        UpdateHealthBar();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount; // Reduce health by the damage amount
        UpdateHealthBar(); // Update the health bar UI

        if (health <= 0)
        {
            health = 0; // Ensure health does not go below zero
            Die();
        }
    }

    void UpdateHealthBar()
    {
        // Update the health bar fill amount based on current health
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = health / 100f; // Assuming max health is 100
        }
    }

    void Die()
    {
        Destroy(gameObject); // Handle player death logic here (e.g., restart the game)
    }
}
