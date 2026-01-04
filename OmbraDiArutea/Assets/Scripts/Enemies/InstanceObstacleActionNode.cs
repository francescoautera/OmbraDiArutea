using UnityEngine;

namespace OmbreDiAretua
{
    [CreateAssetMenu(menuName = "Data/ActionNode/InstanceObstacle", fileName = "InstanceObstacle", order = 0)]

    public class InstanceObstacleActionNode : ActionNode
    {
        [SerializeField] GameObject obstacle;
            
        
        public override void Execute(Enemy enemy, Player player)
        {
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