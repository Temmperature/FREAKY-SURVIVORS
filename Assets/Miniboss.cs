using UnityEngine;

public class Miniboss : MonoBehaviour
{
    public float health = 50f; // Increased health for miniboss
    public float moveSpeed = 2f; // Slightly slower than basic enemy
    private Transform player; // Reference to the player's transform
    private Rigidbody2D rb; // Reference to the miniboss's Rigidbody2D
    private Vector2 movement; // Movement direction
    private int hitCount = 0; // Track how many times the miniboss has been hit
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

        // Flip the miniboss sprite based on movement direction
        if (movement.x > 0)
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        else if (movement.x < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // Move the miniboss
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(15f); // Damage player by 15
                Debug.Log("Miniboss hit! Dealt 15 damage."); // Log that the player was hit
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (cooldownTimer <= 0) // Check if cooldown has expired
        {
            health -= damageAmount; // Reduce health by the damage amount
            hitCount++; // Increment hit count

            // Log the damage received and how many times the miniboss has been hit
            Debug.Log($"Miniboss hit! Damage: {damageAmount}, Total Hits: {hitCount}");

            cooldownTimer = cooldownDuration; // Reset cooldown timer

            if (health <= 0)
            {
                Die(); // Call the Die method if health is zero or less
            }
        }
        else
        {
            Debug.Log("Miniboss is on cooldown and cannot take damage."); // Log when miniboss is on cooldown
        }
    }

    void Die()
    {
        Debug.Log("Miniboss has died."); // Log miniboss death
        Destroy(gameObject); // Destroy the miniboss game object
    }
}
