using UnityEngine;

namespace OmbreDiAretua
{
    [CreateAssetMenu(menuName = "Data/ActionNode/Wait", fileName = "WaitActionNode", order = 0)]
    public class WaitActionNode : ActionNode
    {
        [SerializeField] private float timerWait;
        public override void Execute(Enemy enemy, Player player)
        {
            enemy.SetWaitMode(timerWait);
        }

        public override void Stop()
        {
        }

        public override void Tick(Enemy enemy, Player player)
        {
            
        }
    }
}