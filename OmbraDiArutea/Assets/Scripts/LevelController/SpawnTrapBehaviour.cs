using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OmbreDiAretua
{
   
public class SpawnTrapBehaviour : MonoBehaviour
{
   [SerializeField] protected float timerSpawner;
   [SerializeField] protected AreaTrapSpawner _areaTrapSpawner;
   [SerializeField] protected float radius = 3f;
   protected bool canSpawn = false;
   protected float currentTimerSpawn;
   protected Player _player;
   
   private void Start()
   {
      currentTimerSpawn = 0;
      
   }

   public virtual void InitSpawn()
   {
      currentTimerSpawn = 0;
      canSpawn = true;
      _player = FindObjectOfType<Player>();
   }

   public virtual void StopSpawn()
   {
      currentTimerSpawn = 0;
      canSpawn = false;
      var objectsType = FindObjectsOfType<AreaTrapSpawner>();
      foreach (var areaTrapSpawner in objectsType)
      {
         Destroy(areaTrapSpawner.gameObject);
      }
      
   }


   protected virtual void Update()
   {
      if (!canSpawn)
      {
         return;
      }

      currentTimerSpawn += Time.deltaTime;
      if (currentTimerSpawn > timerSpawner)
      {
         currentTimerSpawn = 0;
         Vector2 puntoRandom = Random.insideUnitCircle * radius;
         Vector3 posizione = new Vector3(_player.transform.position.x +puntoRandom.x,_player.transform.position.y + puntoRandom.y,0);
         var arenaTrap = Instantiate(_areaTrapSpawner, posizione, Quaternion.identity);
         arenaTrap.ShowTrapFeedback();
      }
   }
}
}
