using UnityEngine;

namespace OmbreDiAretua
{
    [CreateAssetMenu(menuName = "Data/CondtionTransition/TrueCondition", fileName = "TrueCondition", order = 0)]

    public class TrueConditionTransition : ConditionTransition
    {
        public override bool CheckTransition(Enemy enemy, Player player)
        {
            
            return true;
            
        }
    }
}