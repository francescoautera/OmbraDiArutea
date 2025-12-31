using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    public abstract class ActionNode : ScriptableObject
    {
        
        protected Enemy _enemy;
        
        public abstract void Execute(Enemy enemy,Player player);

        public abstract void Stop();

        public abstract void Tick(Enemy enemy,Player player);
    }
}