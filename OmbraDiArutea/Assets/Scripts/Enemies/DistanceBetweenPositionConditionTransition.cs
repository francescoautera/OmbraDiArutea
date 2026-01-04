using UnityEngine;

namespace OmbreDiAretua
{
    [CreateAssetMenu(menuName = "Data/CondtionTransition/DistanceBtwnPosition", fileName = "DistanceBtwnPosConditionTransition", order = 0)]
    public class DistanceBetweenPositionConditionTransition : ConditionTransition
    {
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        
        public override bool CheckTransition(Enemy enemy, Player player)
        {
            var distanceCurrent = Vector3.Distance(enemy.transform.position, player.transform.position);
            if (minDistance <= distanceCurrent && distanceCurrent <= maxDistance)
            {
                return true;
            }

            return false;
        }
    }
}