using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{

    public class InstanceEnemyActionNode : ActionNode
    {

        [SerializeField] private List<Enemy> _enemiesToInstance = new List<Enemy>();
        [SerializeField] private int numberToInstance;
        [SerializeField] private GameObject feedbackSpawn;
        private Enemy enemyPosition;
        private OrdeController _ordeController;
        
        private void SetupOrde()
        {
            _ordeController = FindObjectOfType<OrdeController>();
            var list = new List<Enemy>();
            for (int i = 0; i < numberToInstance; i++)
            {
                list.Add(_enemiesToInstance[Random.Range(0,_enemiesToInstance.Count)]);
            }
            
            foreach (var enemy in list)
            {
                Spawn(enemy);
            }
        }
    
        public float raggio = 5f;

        void Spawn(Enemy prefab)
        {
            Vector2 puntoRandom = Random.insideUnitCircle * raggio;
            Vector3 posizione = new Vector3(enemyPosition.transform.position.x +puntoRandom.x,enemyPosition.transform.position.y + puntoRandom.y,0);
            
            var instanceFeedback = Instantiate(feedbackSpawn, posizione, Quaternion.identity);
        
            instanceFeedback.GetComponent<FeedbackSpawn>().Init(() =>
            {
                var enemy = Instantiate(prefab, posizione, Quaternion.identity);
                enemy.Init();
                _ordeController.AddEnemy(enemy);
            });
        }
        
        public override void Execute(Enemy enemy, Player player)
        {
            enemyPosition = enemy;
            SetupOrde();
        }

        public override void Stop()
        {
            
        }

        public override void Tick(Enemy enemy, Player player)
        {
            
        }
    }
}