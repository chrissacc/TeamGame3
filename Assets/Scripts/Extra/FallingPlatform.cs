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
        private bool _isDestroyed;

        private void Start()
        {
            _originalPosition = transform.position; // Store the platform's original position
            _isDestroyed = false; // Platform starts intact
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player") && !_isShaking && !_isDestroyed)
            {
                _fallStartTime = Time.time + fallDelay;
                _isShaking = true;
            }
        }

        private void Update()
        {
            if (_isShaking && Time.time < _fallStartTime)
            {
                Shake();
            }
            else if (_isShaking && Time.time >= _fallStartTime)
            {
                DestroyPlatform();
            }
        }

        private void Shake()
        {
            float shakeOffsetX = Mathf.Sin(Time.time * shakeFrequency) * shakeIntensity;
            float shakeOffsetZ = Mathf.Cos(Time.time * shakeFrequency) * shakeIntensity;

            transform.position = _originalPosition + new Vector3(shakeOffsetX, 0, shakeOffsetZ);
        }

        private void DestroyPlatform()
        {
            gameObject.SetActive(false); // Deactivate the platform
            _isDestroyed = true; // Mark as destroyed
            Debug.Log($"Platform {gameObject.name} destroyed.");
        }

        public void RespawnPlatform()
        {
            _isShaking = false;
            _isDestroyed = false;
            transform.position = _originalPosition; // Reset to original position
            gameObject.SetActive(true); // Reactivate the platform
            Debug.Log($"Platform {gameObject.name} respawned.");
        }
    }
}
