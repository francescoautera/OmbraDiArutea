using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OmbreDiAretua
{
    public class TombTrap : MonoBehaviour
    {
        private bool canSpawn = true;
        [SerializeField] LayerMask _maskToHit;
        [SerializeField] List<EnemyDebuffController> _enemyDebuffControllers = new List<EnemyDebuffController>();
        

        public void Active()
        {
            canSpawn = true;
        }

        public void Stop()
        {
            canSpawn = false;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!canSpawn)
            {
                return;
            }
            
            if ((_maskToHit.value & (1 << other.gameObject.layer)) != 0)
            {
                var enemy = Random.Range(0, _enemyDebuffControllers.Count);
                var instance = Instantiate(_enemyDebuffControllers[enemy], transform.position, Quaternion.identity);
                instance.Execute();
            }
        }
    }

    public abstract class EnemyDebuffController : MonoBehaviour
    {
        public abstract void Execute();


        public void OnAfterAnimation()
        {
            Destroy(gameObject);
        }
    }
}
