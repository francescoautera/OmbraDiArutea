using UnityEngine;

namespace OmbreDiAretua
{
    public class SpawnSpellActionNode : ActionNode
    {
        [SerializeField] private AretuaSpell _aretuaSpell;
        public override void Execute(Enemy enemy, Player player)
        {
            var spell = Instantiate(_aretuaSpell, enemy.transform);
            spell.Execute();
        }

        public override void Stop()
        {
        }

        public override void Tick(Enemy enemy, Player player)
        {
        }
    }
}