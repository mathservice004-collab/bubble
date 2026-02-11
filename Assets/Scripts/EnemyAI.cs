using UnityEngine;

namespace BubbleBobble.AI
{
    public enum EnemyState { Patrolling, Trapped, Angry, Dead }

    public class EnemyAI : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private float jumpChance = 0.01f;
        [SerializeField] private LayerMask groundLayer;

        private Rigidbody2D rb;
        private EnemyState currentState = EnemyState.Patrolling;
        private int direction = 1;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            switch (currentState)
            {
                case EnemyState.Patrolling:
                    Patrol();
                    break;
                case EnemyState.Trapped:
                    // Logic handled by Bubble script usually, or just disable RB
                    break;
            }
        }

        private void Patrol()
        {
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

            // Simple wall/edge detection
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, 0.6f, groundLayer);
            if (hit.collider != null)
            {
                direction *= -1;
            }

            // Pseudo-random jump for classic erratic behavior
            if (Random.value < jumpChance)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10f);
            }
        }

        public void Capture()
        {
            currentState = EnemyState.Trapped;
            rb.simulated = false; // Stop physics while inside bubble
        }

        public void Release(bool killed)
        {
            if (killed)
            {
                currentState = EnemyState.Dead;
                // Drop items logic
                Destroy(gameObject);
            }
            else
            {
                currentState = EnemyState.Angry;
                rb.simulated = true;
                moveSpeed *= 1.5f; // Faster when angry
                // Return to patrolling after some time
            }
        }
    }
}
