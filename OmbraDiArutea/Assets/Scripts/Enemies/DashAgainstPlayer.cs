using UnityEngine;

namespace OmbreDiAretua
{
    public class DashAgainstPlayer : ActionNode
    {
        public Vector3 finalPosition;
        
        public override void Execute(Enemy enemy, Player player)
        {
            _enemy = enemy;
            finalPosition = player.transform.position;
            enemy.SetPositionToCheck(finalPosition);
        }

        public override void Stop()
        {
            _enemy.SetPositionToCheck(Vector3.negativeInfinity);
        }
        
        public override void Tick(Enemy enemy, Player player)
        {
            if (_enemy == null)
            {
                return;
            }
            Vector3 dir = (finalPosition - enemy.transform.position).normalized;
            enemy.transform.position += dir * (enemy.dashMovement * Time.deltaTime); 
        }
    }
}