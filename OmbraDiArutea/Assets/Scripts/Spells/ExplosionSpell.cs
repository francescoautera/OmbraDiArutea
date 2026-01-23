using UnityEngine;

namespace OmbreDiAretua
{
    using UnityEngine;

    public class ExplosionSpell : MonoBehaviour
    {
        [Header("Explosion")]
        [SerializeField] float explosionRadius = 2.5f;
        [SerializeField] LayerMask enemyLayer;

        [Header("Damage")]
        [SerializeField] int damageAgaintsElementalTypeStrong;
        [SerializeField] ElementalType _elementalType;

        [Header("Feedback")]
        [SerializeField] SfxPlayer _soundHitted;
        [SerializeField] DamageShower _damageShower;

        private int _spellDamage;
        private int _playerDamage;

        public void Init(int playerDamage, int spellDamage)
        {
            _spellDamage = spellDamage;
            _playerDamage = playerDamage;
            Explode();
        }
        

        private void Explode()
        {
            _soundHitted.PlayFx();

            Collider2D[] hits = Physics2D.OverlapCircleAll(
                transform.position,
                explosionRadius,
                enemyLayer
            );

            foreach (var hit in hits)
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (!enemy) continue;

                int finalDamage = _spellDamage + _playerDamage;
                bool isSuperEffective = false;
                if (_elementalType == enemy.ElementalType)
                {
                    finalDamage += damageAgaintsElementalTypeStrong;
                    isSuperEffective = true;
                }

                enemy.TakeDamage(finalDamage);

                var damageShower = Instantiate(
                    _damageShower,
                    enemy.transform.position,
                    Quaternion.identity
                );
                damageShower.ShowDamage(finalDamage,isSuperEffective);
            }
        }

        public void OnAnimationFinished()
        {
            Destroy(gameObject);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
#endif
    }

}