using System;
using UnityEngine;

namespace OmbreDiAretua
{
    public class SpellBehaviour : MonoBehaviour
    {
        protected float speed;
        protected float lifetime;
        protected float damage;
        [SerializeField] LayerMask _layerMaskToHit;
        public ElementalType ElementalTypeStrong;
        public int damageAgaintsElementalTypeStrong;
        private float timeInLife;
        private int _playerDamage;

        public void Initialize(SpellStat spellStat,Vector3 mouseWorldPos,int playerDamage)
        {
            _playerDamage = playerDamage;
            speed = spellStat.speed;
            lifetime = spellStat.lifetime;
            damage = spellStat.damage;
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
            if (other.IsTouchingLayers(_layerMaskToHit))
            {
                Execute(other.gameObject);
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

            if (ElementalTypeStrong == enemy.ElementalType)
            {
                damage += damageAgaintsElementalTypeStrong;
            }
            damage += _playerDamage;
            Debug.Log("Danno Fatto : " + damage);      
        }
    }
}