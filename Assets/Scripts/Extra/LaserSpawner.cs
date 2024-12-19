using UnityEngine;

namespace Scenes
{
    public class LaserSpawner : MonoBehaviour
    {
        public GameObject laserPrefab; 
        public float spawnDelay = 1.0f; 
        public float spawnRadius = 1.0f; 
        public bool spawnOnStart = true; 
        private float _spawnTimer; 
        private Transform _playerTransform; 

        private void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            if (spawnOnStart)
            {
                StartSpawning();
            }
        }

        private void Update()
        {
            if (_spawnTimer > 0.0f)
            {
                _spawnTimer -= Time.deltaTime;
            }
            else
            {
                // Reset the spawn timer and spawn a laser
                _spawnTimer = spawnDelay;
                SpawnLaser();
            }
        }

        private void SpawnLaser()
        {
            // Calculate the direction from the spawner to the player
            Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;

            // Generate a random position within the spawn radius
            Vector3 spawnPos = transform.position + directionToPlayer * spawnRadius;

            // Spawn the laser prefab at the random position
            GameObject laser = Instantiate(laserPrefab, spawnPos, Quaternion.identity);

            // Set the laser's direction to be towards the player
            laser.transform.forward = directionToPlayer;
        }

        public void StartSpawning()
        {
            // Reset the spawn timer and start spawning lasers
            _spawnTimer = 0.0f;
        }

        public void StopSpawning()
        {
            // Stop spawning lasers
            _spawnTimer = float.MaxValue;
        }
    }
}