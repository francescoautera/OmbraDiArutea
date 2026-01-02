using UnityEngine;

namespace OmbreDiAretua
{
    public class FireSpellBehaviour : SpellBehaviour
    {
        public override void Execute(GameObject gameObject)
        {
            base.Execute(gameObject);
            var enemy = FindFirstObjectByType<Enemy>();
            enemy.SetFire(timeEffect,damageEffectXSeconds);
        }
    }
}