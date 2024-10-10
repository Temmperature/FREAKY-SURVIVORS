// Kunai.cs (this should be attached to your Kunai prefab)
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private int damage;

    public void SetDamage(int value)
    {
        damage = value; // Set damage from the KunaiWeapon script
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the Kunai hits an enemy
        if (collision.CompareTag("Enemy")) // Make sure your enemies are tagged as "Enemy"
        {
            // Implement damage logic here
            Debug.Log("Kunai hit " + collision.name + " for " + damage + " damage!");
            // Call a TakeDamage method on the enemy, if it exists
            // collision.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject); // Destroy the kunai after hitting
        }
        else
        {
            Destroy(gameObject); // Destroy if it hits something else
        }
    }
}
