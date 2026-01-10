using UnityEngine;

namespace OmbreDiAretua
{
    public class ExplosionSpell : MonoBehaviour
    {
        [SerializeField] int damageAgaintsElementalTypeStrong;
        [SerializeField] ElementalType _elementalType;
        [SerializeField] SfxPlayer _soundHitted;
        [SerializeField] DamageShower _damageShower;
        private int damage;
        private int _playerDamage;

        public void Init(int playerDamage,int spellDamage)
        {
            damage = spellDamage;
            _playerDamage = playerDamage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Enemy>())
            {
                Execute(other.gameObject);
            }
        }
        
        public void Execute(GameObject gameObject)
        {
            var enemy = gameObject.GetComponent<Enemy>();
            if (!enemy)
            {
                return;
            }
            _soundHitted.PlayFx();
            if (_elementalType == enemy.ElementalType)
            {
                damage += damageAgaintsElementalTypeStrong;
            }
            damage += _playerDamage;
            enemy.TakeDamage(damage);
            var damagerShower = Instantiate(_damageShower, enemy.transform.position, Quaternion.identity);
            damagerShower.ShowDamage(damage);
        }
    }
}