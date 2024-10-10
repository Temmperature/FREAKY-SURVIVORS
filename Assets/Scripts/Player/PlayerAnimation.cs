using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator; // Attach your Animator here
    private Vector2 moveDirection;

    void Update()
    {
        HandleMovementAnimations();
    }

    void HandleMovementAnimations()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Check if player is moving
        if (moveDirection.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
            FlipSprite(moveDirection);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void FlipSprite(Vector2 direction)
    {
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
        }
    }
}
