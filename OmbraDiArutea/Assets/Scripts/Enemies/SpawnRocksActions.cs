using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class SpawnRocksActions : ActionNode
    {
        public GameObject rocksToInstantiate;
        public int numberToInstace;
        public float timerBetweenInstances;
        private int currentInstanciated;
        private Player _player;
        
        public override void Execute(Enemy enemy, Player player)
        {
            _player = player;
            TrySpawn(player);
        }

        private void TrySpawn(Player player)
        {
            if (numberToInstace < currentInstanciated)
            {
                currentInstanciated = 0;
                return;
            }

            var rock = Instantiate(rocksToInstantiate, player.transform.position, Quaternion.identity);
            rock.GetComponent<AreaTrapSpawner>().ShowTrapFeedback();
            StartCoroutine(WaitSpawnNext());
        }

        private IEnumerator WaitSpawnNext()
        {
            yield return new WaitForSeconds(timerBetweenInstances);
            currentInstanciated++;
            TrySpawn(_player);
        }

        public override void Stop()
        {
            currentInstanciated = 0;
        }

        public override void Tick(Enemy enemy, Player player)
        {
            
        }
    }
}