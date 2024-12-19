using UnityEngine;

namespace Scenes
{
    public class Laser : MonoBehaviour
    {
        public float speed = 10f; 
        public float knockbackForce = 10f;

        private Rigidbody _rigidbody; 

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                other.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }

            Destroy(gameObject);
        }
    }
}