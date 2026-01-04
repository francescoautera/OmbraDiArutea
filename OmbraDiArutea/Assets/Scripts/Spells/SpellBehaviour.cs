using System;
using UnityEngine;

namespace OmbreDiAretua
{
    public class SpellBehaviour : MonoBehaviour
    {
        protected float speed;
        protected float lifetime;
        protected int damage;
        protected bool isTrapassing;
        protected float timeEffect;
        protected int damageEffectXSeconds;
        [SerializeField] LayerMask _layerMaskToHit;
        public ElementalType ElementalTypeStrong;
        public int damageAgaintsElementalTypeStrong;
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
            timeEffect = spellStat.timeEffectApplied;
            damageEffectXSeconds = spellStat.damageEffectApplied;
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
            var enemy = gameObject.GetComponent<Enemy>();
            if (!enemy)
            {
                return;
            }
            _soundHitted.PlayFx();
            if (ElementalTypeStrong == enemy.ElementalType)
            {
                damage += damageAgaintsElementalTypeStrong;
            }
            damage += _playerDamage;
            enemy.TakeDamage(damage);
            var damagerShower = Instantiate(_damageShowerInstance, enemy.transform.position,
                Quaternion.identity);
            damagerShower.ShowDamage(damage);
            Debug.Log("Danno Fatto : " + damage);      
        }
    }
}