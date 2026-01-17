using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class SpawnEnemyTutorialStep : TutorialStep
    {
        public DialogueData DialogueData;
        private bool RequestMoveEnemy;
        [SerializeField] Enemy enemyToInstance;
        [SerializeField] FeedbackSpawn _feedbackSpawn;
        [SerializeField] private float raggio;
        [SerializeField] private bool destroyAllEnemiesAfterDeath = false;
        private Enemy _currentEnemyInstance;
    
        public override void InitTutorialStep()
        {
            DialogueData.RequestStartDialogue();
            Vector2 puntoRandom = Random.insideUnitCircle * raggio;
            var player = FindFirstObjectByType<Player>();
            Vector3 posizione = new Vector3(player.transform.position.x +puntoRandom.x,player.transform.position.y + puntoRandom.y,0);

            var instanceFeedback = Instantiate(_feedbackSpawn, posizione,Quaternion.identity);
        
            instanceFeedback.Init(() =>
            {
                var enemy = Instantiate(enemyToInstance, posizione, Quaternion.identity);
                enemy.OnDeath += OnDeath;
                _currentEnemyInstance = enemy;
            });
        }

        private void OnDeath(Enemy obj)
        {
            if (destroyAllEnemiesAfterDeath)
            {
                var en = FindObjectsOfType<Enemy>();
                foreach (var enemy in en)
                {
                    Destroy(enemy.gameObject);
                }
            }
            StartCoroutine(Wait());
        }

        public override void UnlcokStep()
        {
            RequestMoveEnemy = true;
            _currentEnemyInstance.Init();
        }

     

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.3f);
            OnEndTutorialStep?.Invoke();
        }
    
    }
}