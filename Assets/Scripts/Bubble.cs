using UnityEngine;

namespace BubbleBobble.Core
{
    public class Bubble : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float horizontalSpeed = 10f;
        [SerializeField] private float floatSpeed = 2f;
        [SerializeField] private float lifetime = 5f;
        [SerializeField] private float horizontalMoveDuration = 0.5f;

        private Rigidbody2D rb;
        private float spawnTime;
        private bool isFloating = false;
        private Vector2 direction;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spawnTime = Time.time;
            
            // Set initial direction based on player rotation
            direction = transform.right; 
        }

        private void Start()
        {
            // Initial burst forward
            rb.velocity = direction * horizontalSpeed;
        }

        private void Update()
        {
            if (!isFloating && Time.time > spawnTime + horizontalMoveDuration)
            {
                StartFloating();
            }

            if (Time.time > spawnTime + lifetime)
            {
                Pop();
            }
        }

        private void StartFloating()
        {
            isFloating = true;
            rb.velocity = Vector2.up * floatSpeed;
            // Optionally add some sine wave horizontal movement here for classic feel
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                TrapEnemy(other.gameObject);
            }
            else if (other.CompareTag("Player") && isFloating)
            {
                Pop();
            }
        }

        private void TrapEnemy(GameObject enemy)
        {
            // Logic to disable enemy and parent it to the bubble
            // enemy.GetComponent<EnemyAI>().Capture();
            // enemy.transform.SetParent(transform);
            // enemy.transform.localPosition = Vector3.zero;
            
            isFloating = true;
            rb.velocity = Vector2.up * (floatSpeed * 0.5f); // Slower when carrying weight
        }

        public void Pop()
        {
            // Spawn particles, handle trapped enemy release/death
            Destroy(gameObject);
        }
    }
}
