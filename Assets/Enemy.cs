using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 30f; // Set initial health for the enemy
    public float moveSpeed = 3f; // Speed at which the enemy moves
    private Transform player; // Reference to the player's transform
    private Rigidbody2D rb; // Reference to the enemy's Rigidbody2D
    private Vector2 movement; // Movement direction
    private int hitCount = 0; // Track how many times the enemy has been hit
    private float cooldownDuration = 0.5f; // Cooldown duration for receiving damage
    private float cooldownTimer = 0f; // Timer to track cooldown

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        MoveTowardsPlayer(); // Call the method to move towards the player

        // Update cooldown timer
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime; // Decrease cooldown timer
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized; // Calculate direction towards the player
        movement = direction;

        // Flip the enemy sprite based on movement direction
        if (movement.x > 0)
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        else if (movement.x < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // Move the enemy
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10f); // Damage player by 10
                Debug.Log("Player hit! Dealt 10 damage."); // Log that the player was hit
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (cooldownTimer <= 0) // Check if cooldown has expired
        {
            health -= damageAmount; // Reduce health by the damage amount
            hitCount++; // Increment hit count

            // Log the damage received and how many times the enemy has been hit
            Debug.Log($"Enemy hit! Damage: {damageAmount}, Total Hits: {hitCount}");
            Debug.Log($"Enemy current health: {health}");

            cooldownTimer = cooldownDuration; // Reset cooldown timer

            if (health <= 0)
            {
                Die(); // Call the Die method if health is zero or less
            }
        }
        else
        {
            Debug.Log("Enemy is on cooldown and cannot take damage."); // Log when enemy is on cooldown
        }
    }

    void Die()
    {
        Debug.Log("Enemy has died."); // Log enemy death
        Destroy(gameObject); // Destroy the enemy game object
    }
}
