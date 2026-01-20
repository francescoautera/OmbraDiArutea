using UnityEngine;

namespace OmbreDiAretua
{
    public class SpawnerChaserObstacle : SpawnTrapBehaviour
    {
        [SerializeField] ChaserObstacle _chaser;
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
                Vector2 puntoRandom = (Random.insideUnitCircle * radius);
                puntoRandom += new Vector2(radius / 2, radius / 2);
                Vector3 posizione = new Vector3(_player.transform.position.x +puntoRandom.x,_player.transform.position.y + puntoRandom.y,0);
                var arenaTrap = Instantiate(_chaser, posizione, Quaternion.identity);
                arenaTrap.Init(_player);
            }
      
        }

        public override void StopSpawn()
        {
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