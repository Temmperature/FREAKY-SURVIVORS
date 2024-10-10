using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerHealth playerHealth; // Reference to the PlayerHealth script
    public Image healthBar; // Reference to the health bar UI Image

    void Update()
    {
        if (playerHealth != null)
        {
            // Update the health bar's fill amount based on the player's current health
            healthBar.fillAmount = playerHealth.CurrentHealth / 100f; // Assuming max health is 100
        }
    }
}
