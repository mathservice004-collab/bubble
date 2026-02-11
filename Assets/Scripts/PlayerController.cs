using UnityEngine;
using UnityEngine.InputSystem;

namespace BubbleBobble.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float jumpForce = 12f;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;

        [Header("Bubble Shooting")]
        [SerializeField] private GameObject bubblePrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float shootCooldown = 0.25f;

        private Rigidbody2D rb;
        private Vector2 moveInput;
        private bool isGrounded;
        private float lastShootTime;
        private bool facingRight = true;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            // Ensure gravity and constraints are set for 2D platformer
            rb.gravityScale = 3f;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        private void Update()
        {
            CheckGround();
            ApplyMovement();
        }

        private void CheckGround()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        public void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>();
            
            if (moveInput.x > 0 && !facingRight) Flip();
            else if (moveInput.x < 0 && facingRight) Flip();
        }

        public void OnJump(InputValue value)
        {
            if (value.isPressed && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        public void OnFire(InputValue value)
        {
            if (value.isPressed && Time.time >= lastShootTime + shootCooldown)
            {
                ShootBubble();
                lastShootTime = Time.time;
            }
        }

        private void ApplyMovement()
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        }

        private void ShootBubble()
        {
            if (bubblePrefab == null || shootPoint == null) return;
            
            GameObject bubble = Instantiate(bubblePrefab, shootPoint.position, Quaternion.identity);
            // logic for bubble direction will be handled by the bubble's own script
        }

        private void Flip()
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
    }
}
