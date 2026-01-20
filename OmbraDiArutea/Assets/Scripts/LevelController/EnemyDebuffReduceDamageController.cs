using UnityEngine;

namespace OmbreDiAretua
{
    public class EnemyDebuffReduceDamageController : EnemyDebuffController
    {
        [SerializeField] float timer;
        [SerializeField] int reduceDamage = 2;
        public override void Execute()
        {
            FindFirstObjectByType<SpellManager>().ReduceDamage(timer,reduceDamage);
        }
    }
}