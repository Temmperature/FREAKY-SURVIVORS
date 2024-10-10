using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float speed = 0.1f;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPos = player.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, speed);
        }
    }
}
