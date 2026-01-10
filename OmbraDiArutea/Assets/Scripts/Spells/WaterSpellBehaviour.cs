using System;
using UnityEngine;

namespace OmbreDiAretua
{
    public class WaterSpellBehaviour : SpellBehaviour
    {
        [SerializeField] private ExplosionSpell _explosionSpell;
        
        public override void Execute(GameObject gameObject)
        {
            if (gameObject.GetComponent<Enemy>())
            {
                var explosion = Instantiate(_explosionSpell, gameObject.transform.position, Quaternion.identity);
                explosion.Init(_playerDamage,damage);
            }
        }
    }
}