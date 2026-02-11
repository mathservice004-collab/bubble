using UnityEngine;

namespace BubbleBobble.Core
{
    /// <summary>
    /// Implements the classic screen wrapping logic where objects falling through the bottom 
    /// appear at the top, and potentially side-to-side wrapping if the level design requires it.
    /// </summary>
    public class ScreenWrapper : MonoBehaviour
    {
        [Header("Boundary Settings")]
        [SerializeField] private float topBoundary = 10f;
        [SerializeField] private float bottomBoundary = -10f;
        [SerializeField] private float leftBoundary = -18f;
        [SerializeField] private float rightBoundary = 18f;
        
        [Header("Wrapping Toggles")]
        public bool wrapVertical = true;
        public bool wrapHorizontal = false;

        private void LateUpdate()
        {
            Vector3 pos = transform.position;

            if (wrapVertical)
            {
                if (pos.y < bottomBoundary)
                {
                    pos.y = topBoundary;
                }
                else if (pos.y > topBoundary)
                {
                    pos.y = bottomBoundary;
                }
            }

            if (wrapHorizontal)
            {
                if (pos.x < leftBoundary)
                {
                    pos.x = rightBoundary;
                }
                else if (pos.x > rightBoundary)
                {
                    pos.x = leftBoundary;
                }
            }

            transform.position = pos;
        }
    }
}
