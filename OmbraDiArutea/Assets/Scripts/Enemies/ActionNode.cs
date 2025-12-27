using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    [CreateAssetMenu(menuName = "Data/ActionNode", fileName = "ActionNode", order = 0)]
    public class ActionNode : ScriptableObject
    {
        public EnemyAction currentAction;
        public List<Transitions> TransitionsList = new List<Transitions>();

        public ActionNode TryGetNewNode(Enemy enemy)
        {
            foreach (var transitions in TransitionsList)
            {
                if (transitions.Check(enemy))
                {
                    return transitions.actionToArrive;
                }
            }

            return null;
        }

    }
}