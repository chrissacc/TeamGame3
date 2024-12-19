using UnityEngine;

namespace Scenes
{
    public class FlippingPlatform : MonoBehaviour
    {
        public float flipSpeed = 360f; // Speed of rotation during a flip
        public float flipDuration = 1f; // Duration of a flip
        public float flipDelay = 0.5f; // Delay between flips

        private bool _isFlipping; // Is the platform currently flipping?
        private Quaternion _targetRotation; // Target rotation during a flip
        private Quaternion _initialRotation; // Initial rotation of the platform
        private float _flipTimer; // Timer to handle the duration of flips
        private float _flipDelayTimer; // Timer to handle delay between flips

        private void Start()
        {
            _initialRotation = transform.rotation;
            StartFlipCycle(); // Begin the flipping cycle
        }

        private void Update()
        {
            if (_isFlipping)
            {
                // Rotate towards
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, flipSpeed * Time.deltaTime);

                // Check if the flip comp
                if (transform.rotation == _targetRotation)
                {
                    _flipTimer -= Time.deltaTime;
                    if (_flipTimer <= 0f)
                    {
                        // Reset the timer
                        _flipDelayTimer = flipDelay;
                        _isFlipping = false;
                        _targetRotation = _initialRotation;
                    }
                }
            }
            else
            {
                // Handle delay between flips
                _flipDelayTimer -= Time.deltaTime;
                if (_flipDelayTimer <= 0f)
                {
                    StartFlipCycle();
                }
            }
        }
        //set flip time and dur
        private void StartFlipCycle()
        {
            // Set up the next flip
            _flipTimer = flipDuration;
            _targetRotation = transform.rotation * Quaternion.Euler(Vector3.right * 180f);
            _isFlipping = true;
        }
    }
}
