using System;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private List<State> _states = new List<State>();
        [SerializeField] ActionNode _currentActionNode;
        private State _currentState;
        private Enemy _currentEnemy;
        private bool isUnlock;
        private Player _player;
        
        public void Init(Enemy enemy)
        {
            _player = FindFirstObjectByType<Player>();
            _currentEnemy = enemy;
            _currentState = _states[0];
            _currentActionNode = _states[0].actionNode;
            _currentActionNode.Execute(_currentEnemy,_player);
            isUnlock = true;
        }

        private void Update()
        {
            if(!isUnlock && _currentEnemy == null && _currentActionNode == null)
                return;
            
            _currentActionNode.Tick(_currentEnemy,_player);
            var newAction = _currentState.TryGetNewNode(_currentEnemy,_player);

            if (newAction != null)
            {
                foreach (var state in _states)
                {
                    if (state.actionNode == newAction && newAction != _currentActionNode)
                    {
                        _currentActionNode.Stop();
                        _currentState = state;
                        _currentActionNode = newAction;
                        _currentActionNode.Execute(_currentEnemy,_player);
                        return;
                    }
                }
              
            }
        }

        public void Unlock() => isUnlock = true;

        public void Lock() => isUnlock = false;
    }

    [Serializable]
    public class State
    {
        public ActionNode actionNode;
        public List<Transitions> _transitions = new();
        
        public ActionNode TryGetNewNode(Enemy enemy,Player player)
        {
            foreach (var transitions in _transitions)
            {
                if (transitions.Check(enemy,player))
                {
                    return transitions.actionToArrive;
                }
            }

            return null;
        }
    }
}