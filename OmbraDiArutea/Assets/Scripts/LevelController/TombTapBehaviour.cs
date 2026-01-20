using System.Collections.Generic;

namespace OmbreDiAretua
{
    public class TombTapBehaviour : SpawnTrapBehaviour
    {
        public List<TombTrap> tombTraps = new List<TombTrap>();

        public override void StopSpawn()
        {
            foreach (var aTombTrap in tombTraps)
            {
                aTombTrap.Stop();
            }
        }

        public override void InitSpawn()
        {
            foreach (var aTombTrap in tombTraps)
            {
                aTombTrap.Active();
            }
        }

        protected override void Update()
        {
            
        }
    }
}