using UnityEngine;

public class SwordWeapon : WeaponController
{
    public Transform swordTransform; // Reference to the sword's transform
    public LayerMask enemyLayer; // Layer for enemies
    public Animator animator; // Reference to the Animator component
    private bool hasHitEnemy = false; // Flag to check if the enemy has already been hit

    [Header("Attack Settings")]
    [SerializeField] private PolygonCollider2D attackCollider; // Reference to the PolygonCollider2D for the attack hitbox

    public override void Update()
    {
        base.Update();

        if (CanAttack())
        {
            Attack();
        }
    }

    void Attack()
    {
        // Trigger the sword attack animation
        if (animator != null)
        {
            animator.SetBool("isAttacking", true); // Assuming the parameter in Animator is named "isAttacking"
        }

        // Enable the attack collider for a short duration
        attackCollider.enabled = true;

        // Reset the hit enemy flag
        hasHitEnemy = false; // Reset to allow hitting multiple enemies
        Invoke("DisableCollider", 0.2f); // Adjust the duration as needed
    }

    void DisableCollider()
    {
        attackCollider.enabled = false; // Disable the attack collider
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !hasHitEnemy)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Access damage from the base class
                hasHitEnemy = true; // Set the flag to true to prevent double hitting
                Debug.Log("Enemy hit: " + enemy.name); // Log enemy hit
            }
        }
    }
}
