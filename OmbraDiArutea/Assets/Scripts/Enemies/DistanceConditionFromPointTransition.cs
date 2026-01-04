using UnityEngine;

namespace OmbreDiAretua
{
    [CreateAssetMenu(menuName = "Data/CondtionTransition/DistanceFromPoint", fileName = "DistanceFromPoint", order = 0)]
    public class DistanceConditionFromPointTransition : ConditionTransition
    {
        [SerializeField] float distance;
        [SerializeField] bool checkLessThan;
        
        public override bool CheckTransition(Enemy enemy,Player player)
        {
            var distanceCurrent = enemy.GetPositionToCheckDistance();
            if (distanceCurrent < 0)
            {
                return false;
            }
            
            if (checkLessThan)
            {
                return distance > distanceCurrent;
            }
            
            return distance < distanceCurrent;
        }
    }
}