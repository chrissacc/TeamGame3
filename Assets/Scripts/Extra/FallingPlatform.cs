using UnityEngine;

namespace Scenes
{
    public class FallingPlatform : MonoBehaviour
    {
        public float fallDelay = 0.5f; 
        public float shakeIntensity = 0.1f; 
        public float shakeFrequency = 20f; 

        private bool _isShaking; 
        private float _fallStartTime; 
        private Vector3 _originalPosition;

        private void Start()
        {
            _originalPosition = transform.position; // Store the platform's original position
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player") && !_isShaking)
            {
                // Schedule the destruction and start shaking
                _fallStartTime = Time.time + fallDelay;
                _isShaking = true;
            }
        }

        private void Update()
        {
            if (_isShaking && Time.time < _fallStartTime)
            {
                Shake(); // Apply shaking effect
            }
            else if (_isShaking && Time.time >= _fallStartTime)
            {
                DestroyPlatform(); // Destroy the platform after shaking
            }
        }

        private void Shake()
        {
            // Create a shaking effect using sine wave
            float shakeOffsetX = Mathf.Sin(Time.time * shakeFrequency) * shakeIntensity;
            float shakeOffsetZ = Mathf.Cos(Time.time * shakeFrequency) * shakeIntensity;

            // Apply the offset to the platform's original position
            transform.position = _originalPosition + new Vector3(shakeOffsetX, 0, shakeOffsetZ);
        }

        private void DestroyPlatform()
        {
            Destroy(gameObject); // Destroy the platform object
        }
    }
}