using UnityEngine;

namespace OmbreDiAretua
{
    public class EscapeActionNode : ActionNode
    {
        public override void Execute(Enemy enemy,Player player)
        {
            _enemy = enemy;
            
            if (_enemy == null)
            {
                return;
            }
            
            Vector3 dir = -(player.transform.position - enemy.transform.position).normalized;
            enemy.transform.position += dir * (enemy.speedMovement * Time.deltaTime);
        }
        

        public override void Stop()
        {
            _enemy = null;
        }

        public override void Tick(Enemy enemy,Player player)
        {
            if (_enemy == null)
            {
                return;
            }
            Vector3 dir = -(player.transform.position - enemy.transform.position).normalized;
            enemy.transform.position += dir * (enemy.speedMovement * Time.deltaTime);
        }
    }
}