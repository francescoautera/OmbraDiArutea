using UnityEngine;

namespace OmbreDiAretua
{
    public class EnemyDebuffSlowController : EnemyDebuffController
    {
        [SerializeField] private float timerSlow;
        [SerializeField] private float velocityMultiplier = 0.8f;
        public override void Execute()
        {
            FindFirstObjectByType<PlayerMovement>().DebuffSlowPlayer(timerSlow,velocityMultiplier);    
        }
    }
}