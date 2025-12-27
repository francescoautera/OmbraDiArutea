using System;

namespace OmbreDiAretua
{
    [Serializable]
    public class Transitions
    {
        public ConditionTransition ConditionTransition;
        public ActionNode actionToArrive;

        public bool Check(Enemy enemy)
        {
            return ConditionTransition.CheckTransition(enemy);
        }
        
    }
}