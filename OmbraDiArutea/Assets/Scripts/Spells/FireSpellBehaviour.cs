using UnityEngine;

namespace OmbreDiAretua
{
    public class FireSpellBehaviour : SpellBehaviour
    {
        public override void Execute(GameObject gameObject)
        {
            base.Execute(gameObject);
            var enemy = gameObject.GetComponent<Enemy>();
            enemy.SetFire(timeEffect,damageEffectXSeconds);
        }
    }
}