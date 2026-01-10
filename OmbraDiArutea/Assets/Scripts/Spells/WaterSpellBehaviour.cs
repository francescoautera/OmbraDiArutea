using System;
using UnityEngine;

namespace OmbreDiAretua
{
    public class WaterSpellBehaviour : SpellBehaviour
    {
        [SerializeField] private ExplosionSpell _explosionSpell;
        
        public override void Execute(GameObject gameObject)
        {
            base.Execute(gameObject);
        }
    }
}