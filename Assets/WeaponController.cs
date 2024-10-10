using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] protected float damage; // Damage dealt by the weapon
    [SerializeField] protected float attackSpeed; // Speed of the attack
    [SerializeField] protected float cooldown; // Cooldown duration between attacks
    private float currentCooldown; // Tracks the remaining cooldown time

    public virtual void Update()
    {
        // Decrease the current cooldown by the time passed since the last frame
        currentCooldown -= Time.deltaTime;
    }

    // Check if the weapon is ready to attack
    public bool CanAttack()
    {
        return currentCooldown <= 0;
    }

    // Reset the cooldown timer
    public void ResetCooldown()
    {
        currentCooldown = cooldown;
    }
}
