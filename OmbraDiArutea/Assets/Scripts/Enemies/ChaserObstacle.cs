using UnityEngine;

namespace OmbreDiAretua
{
    public class ChaserObstacle : MonoBehaviour
    {
        public float lifeTime;
        public int damage;
        public float speed;
        private Player _player;
        private float currentLifeTime;
        
        public void Init(Player position)
        {
            _player = position;
            Vector2 direction = (position.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Movimento
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = direction * speed;
        }


        private void Update()
        {
            if (_player == null)
            {
                return;
            }

            currentLifeTime += Time.deltaTime;
            if (currentLifeTime > lifeTime)
            {
                Destroy(gameObject);
                return;
            }
            Vector2 direction = (_player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Movimento
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = direction * speed;
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player)
            {
                player.AddHealth(-damage);
                Destroy(gameObject);
            }
        }
    }
}