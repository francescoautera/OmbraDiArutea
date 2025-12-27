using UnityEngine;

namespace OmbreDiAretua
{

    public abstract class ConditionTransition : ScriptableObject
    {
        public abstract bool CheckTransition(Enemy enemy);
    }
}