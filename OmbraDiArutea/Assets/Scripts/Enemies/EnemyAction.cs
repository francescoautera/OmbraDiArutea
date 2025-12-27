using UnityEngine;

namespace OmbreDiAretua
{
    public abstract class EnemyAction : ScriptableObject
    {
        protected Enemy _enemy;
        
        public abstract void Execute(Enemy enemy);

        public abstract void Stop();
    }
}