using UnityEngine;

namespace OmbreDiAretua
{
    public class RockSpellBehaviour : SpellBehaviour
    {
        
        
        public override void Execute(GameObject gameObject)
        {
            base.Execute(gameObject);
            var enemy = gameObject.GetComponent<Enemy>();
            enemy.SetSlow(timeEffect);
        }
    }
}