using UnityEngine;

namespace OmbreDiAretua
{
    [CreateAssetMenu(menuName = "Data/CondtionTransition/Wait", fileName = "WaitCheck", order = 0)]
    public class WaitConditionTransition : ConditionTransition
    {
        
        public override bool CheckTransition(Enemy enemy,Player player)
        {
            return !enemy.IsInWaitMode();
        }
    }
}