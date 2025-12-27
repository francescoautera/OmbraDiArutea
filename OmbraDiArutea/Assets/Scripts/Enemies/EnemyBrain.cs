using UnityEngine;

namespace OmbreDiAretua
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private ActionNode firstActionNode;
        private ActionNode _currentActionNode;
        private Enemy _currentEnemy;
        private bool isUnlock;
        
        public void Init(Enemy enemy)
        {
            _currentEnemy = enemy;
            _currentActionNode = firstActionNode;
            _currentActionNode.currentAction.Execute(_currentEnemy);
        }

        private void Update()
        {
            if(!isUnlock && _currentEnemy == null && _currentActionNode == null)
                return;
            
            var newAction = _currentActionNode.TryGetNewNode(_currentEnemy);

            if (newAction != null)
            {
                _currentActionNode.currentAction.Stop();
                _currentActionNode = newAction;
                newAction.currentAction.Execute(_currentEnemy);
            }
        }

        public void Unlock() => isUnlock = true;

        public void Lock() => isUnlock = false;
    }
}