using System;

namespace OmbreDiAretua
{
    [Serializable]
    public class Transitions
    {
        public ConditionTransition ConditionTransition;
        public ActionNode actionToArrive;

        public bool Check(Enemy enemy,Player player)
        {
            return ConditionTransition.CheckTransition(enemy,player);
        }
        
        
        
    }
}