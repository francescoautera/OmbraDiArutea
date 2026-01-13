using UnityEngine;

namespace OmbreDiAretua
{

    public class InstanceObstacleActionNode : ActionNode
    {
        [SerializeField] GameObject obstacle;
            
        
        public override void Execute(Enemy enemy, Player player)
        {
            if (player == null)
            {
                player = FindFirstObjectByType<Player>();
            }
            Instantiate(obstacle, enemy.transform.position, Quaternion.identity);
            obstacle.GetComponent<ChaserObstacle>().Init(player);
        }

        public override void Stop()
        {
        }

        public override void Tick(Enemy enemy, Player player)
        {
        }
    }
}