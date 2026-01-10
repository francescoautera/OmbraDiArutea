using UnityEngine;

namespace OmbreDiAretua
{
    
    [CreateAssetMenu(menuName = "Data/ActionNode/Escape", fileName = "EscapeActionNode", order = 0)]

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
            Debug.Log("Escape");
            Vector3 dir = -(player.transform.position - enemy.transform.position).normalized;
            enemy.transform.position += dir * (enemy.speedMovement * Time.deltaTime);
        }
    }
}