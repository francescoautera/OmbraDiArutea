using UnityEngine;

namespace OmbreDiAretua
{
    public class ShootActionNode : ActionNode
    {

        public SpellData _enemySpellData;
        
        public override void Execute(Enemy enemy, Player player)
        {
            
            var spellStat = _enemySpellData.GetSpellStat();
            var instanceSpell = Instantiate(_enemySpellData.instanceSpell,enemy.transform.position,Quaternion.identity);
            instanceSpell.GetComponent<EnemySpellBehaviour>().Initialize(spellStat,player.transform.position,enemy.damage);
            
        }
        

        public override void Stop()
        {
        }

        public override void Tick(Enemy enemy, Player player)
        {
        }
    }
}