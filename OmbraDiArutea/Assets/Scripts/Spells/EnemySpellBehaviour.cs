using UnityEngine;

namespace OmbreDiAretua
{
    public class EnemySpellBehaviour : MonoBehaviour
    {
        protected float speed;
        protected float lifetime;
        protected int damage;
        protected bool isTrapassing;
        [SerializeField] LayerMask _layerMaskToHit;
        [SerializeField] private DamageShower _damageShowerInstance;
        [SerializeField] private SfxPlayer _soundHitted;
        private float timeInLife;
        private int _playerDamage;

        public void Initialize(SpellStat spellStat,Vector3 mouseWorldPos,int playerDamage)
        {
            _playerDamage = playerDamage;
            speed = spellStat.speed;
            lifetime = spellStat.lifetime;
            damage = spellStat.damage;
            isTrapassing = spellStat.hasTrapassing;
            Vector2 direction = (mouseWorldPos - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Movimento
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = direction * speed;
        }

        private void Update()
        {
            timeInLife += Time.deltaTime;
            if (timeInLife >= lifetime)
            {
                Destroy(gameObject);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.gameObject);
            if ((_layerMaskToHit.value & (1 << other.gameObject.layer)) != 0)
            {
                Debug.Log("Enter");
                Execute(other.gameObject);
                if (isTrapassing)
                {
                    return;
                }
                Destroy(gameObject);
            }
        }

        public virtual void Execute(GameObject gameObject)
        {
            var player = gameObject.GetComponent<Player>();
            if (!player)
            {
                return;
            }

            if (player.IsInvincibily)
            {
                return;
            }
            _soundHitted.PlayFx();
            var spellDamage = damage + _playerDamage;
            player.TakeDamage(spellDamage);
            var damagerShower = Instantiate(_damageShowerInstance, player.transform.position,
                Quaternion.identity);
            damagerShower.ShowDamage(spellDamage);
        }
        
    }
}