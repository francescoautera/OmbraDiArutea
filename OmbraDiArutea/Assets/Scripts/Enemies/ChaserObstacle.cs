using UnityEngine;

namespace OmbreDiAretua
{
    public class ChaserObstacle : MonoBehaviour
    {
        public float lifeTime;
        public int damage;
        public float speed;
        [SerializeField] private DamageShower _damageShower;
        Player _player;
        float currentLifeTime;
        
        public void Init(Player position)
        {
            _player = position;
            Vector3 dir = (_player.transform.position - transform.position).normalized;
            transform.position += dir * (speed * Time.deltaTime);
        }


        private void Update()
        {
            if (_player == null)
            {
                _player = FindFirstObjectByType<Player>();
                return;
            }

            currentLifeTime += Time.deltaTime;
            if (currentLifeTime > lifeTime)
            {
                Destroy(gameObject);
                return;
            }
            
            Vector3 dir = (_player.transform.position - transform.position).normalized;
            transform.position += dir * (speed * Time.deltaTime);
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player)
            {
                var showerDamage = Instantiate(_damageShower, player.transform.position, Quaternion.identity);
                showerDamage.ShowDamage(damage);
                player.AddHealth(-damage);
                Destroy(gameObject);
            }
        }
    }
}