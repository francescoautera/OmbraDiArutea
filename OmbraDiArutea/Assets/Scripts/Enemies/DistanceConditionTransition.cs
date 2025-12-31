using UnityEngine;

namespace OmbreDiAretua
{
    [CreateAssetMenu(menuName = "Data/CondtionTransition/Distance", fileName = "DistanceConditionTransition", order = 0)]
    public class DistanceConditionTransition : ConditionTransition
    {
        [SerializeField] float distance;
        [SerializeField] bool checkLessThan;
        
        public override bool CheckTransition(Enemy enemy,Player player)
        {
            var distanceCurrent = Vector3.Distance(enemy.transform.position, player.transform.position);
            if (checkLessThan)
            {
                return distance > distanceCurrent;
            }
            
            return distance < distanceCurrent;
        }
    }
}