using UnityEngine;

namespace OmbreDiAretua
{
    public class MoveRandomInZone : ActionNode
    {

        public float raggio;
        public Vector3 finalPosition;
        
        public override void Execute(Enemy enemy, Player player)
        {
            _enemy = enemy;
            Vector2 puntoRandom = Random.insideUnitCircle * raggio;
            Vector3 posizione = new Vector3(enemy.transform.position.x +puntoRandom.x,enemy.transform.position.y + puntoRandom.y,0);
            finalPosition = posizione;
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
            enemy.transform.position += dir * (enemy.speedMovement * Time.deltaTime); 
        }
    }
}