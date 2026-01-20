using UnityEngine;

namespace OmbreDiAretua
{
    public class EnemyDebuffVenomController : EnemyDebuffController
    {
        [SerializeField] float timerSlow;
        [SerializeField] float tickDamage = 10f;
        public override void Execute()
        {
            FindFirstObjectByType<Player>().ApplyStatus(timerSlow, tickDamage);
        }
    }
}