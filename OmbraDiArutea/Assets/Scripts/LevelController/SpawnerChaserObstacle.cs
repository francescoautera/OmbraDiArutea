using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    public class SpawnerChaserObstacle : SpawnTrapBehaviour
    {
        [SerializeField] ChaserObstacle _chaser;
        [SerializeField] GameObject _feedback;
        [SerializeField] List<Transform> positionToSpawn = new(); 
        protected override void Update()
        {
            if (!canSpawn)
            {
                return;
            }

            currentTimerSpawn += Time.deltaTime;
            if (currentTimerSpawn > timerSpawner)
            {
                currentTimerSpawn = 0;
                StartCoroutine(Setup());
            }
      
        }

        IEnumerator Setup()
        {
            var posizione = positionToSpawn[Random.Range(0, positionToSpawn.Count)];
            var feedback = Instantiate(_feedback, posizione.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            var arenaTrap = Instantiate(_chaser, posizione.position, Quaternion.identity);
            arenaTrap.Init(_player);
            Destroy(feedback);
        }

        public override void StopSpawn()
        {
            StopAllCoroutines();
            currentTimerSpawn = 0;
            canSpawn = false;
            var objectsType = FindObjectsOfType<ChaserObstacle>();
            foreach (var areaTrapSpawner in objectsType)
            {
                Destroy(areaTrapSpawner.gameObject);
            }
        }
    }
}