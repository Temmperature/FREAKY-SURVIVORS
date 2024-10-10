using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        ChasePlayer();
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to the player here
            Debug.Log("Enemy has damaged the player");
        }
    }
}
