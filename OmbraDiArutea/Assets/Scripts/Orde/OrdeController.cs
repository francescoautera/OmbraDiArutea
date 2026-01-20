using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OmbreDiAretua
{
    
public class OrdeController : MonoBehaviour
{
    public SerializedDictionary<int, List<Enemy>> ordes = new SerializedDictionary<int, List<Enemy>>();
    public OrdeViewer _Viewer;
    private PowerUpController _PowerUpController;
    public FeedbackSpawn _FeedbackSpawn;
    private int currentOrde = 1;
    private List<Enemy> _currentsEnemies = new List<Enemy>();
    public Action OnCompleted;
    public Transform enemySpawn;
    private Player _player;
    
    private void Start()
    {
        _PowerUpController = FindObjectOfType<PowerUpController>();
    }

    public void ShowOrde()
    {
        _Viewer.ShowNextOrde(currentOrde, SetupOrde);
    }

    private void SetupOrde()
    {
        var list = new List<Enemy>(ordes[currentOrde]);
        foreach (var enemy in list)
        {
            Spawn(enemy);
        }
        _Viewer.ShowRemainEnemies(list.Count);
        var spellTrap = FindObjectOfType<SpawnTrapBehaviour>();
        if (spellTrap)
        {
            spellTrap.InitSpawn();
        }

    }
    
    public float raggio = 5f;

    void Spawn(Enemy prefab)
    {
        if (!_player)
        {
            _player = FindFirstObjectByType<Player>();
        }
        Vector2 puntoRandom = Random.insideUnitCircle * raggio;
        Vector3 posizione = new Vector3(_player.transform.position.x + puntoRandom.x,_player.transform.position.y + puntoRandom.y,0);
        var instanceFeedback = Instantiate(_FeedbackSpawn, posizione, Quaternion.identity);
        
        instanceFeedback.Init(() =>
        {
            var enemy = Instantiate(prefab, posizione, Quaternion.identity);
            enemy.Init();
            enemy.transform.SetParent(enemySpawn);
            enemy.OnDeath += OnDeath;
            _currentsEnemies.Add(enemy);
        });
       
    }

    private void OnDeath(Enemy enemy)
    {
        enemy.OnDeath -= OnDeath;
        _currentsEnemies.Remove(enemy);
        if (_currentsEnemies.Count <= 0)
        {
            _Viewer.CloseShowRemainEnemies();
            var spellTrap = FindObjectOfType<SpawnTrapBehaviour>();
            if (spellTrap)
            {
                spellTrap.StopSpawn();
            }
            StartCoroutine(Wait());
            IEnumerator Wait()
            {
                yield return new WaitForSeconds(1.2f);
                if (TryCheckOrde())
                {
                    ChangeOrde();
                    yield break;
                }

                var obstacles = FindObjectsOfType<ChaserObstacle>();
                foreach (var chaserObstacle in obstacles)
                {
                    Destroy(chaserObstacle.gameObject);
                }
                if (!_PowerUpController)
                {
                    _PowerUpController = FindFirstObjectByType<PowerUpController>();
                }
                _PowerUpController.ShowPowerUp(currentOrde,ChangeOrde);    
            }
            return;
        }

        
        _Viewer.ShowRemainEnemies(_currentsEnemies.Count);
    }

    private bool TryCheckOrde()
    {
        var nextOrde = currentOrde + 1;
        if (nextOrde > ordes.Count)
        {
            return true;
        }

        return false;
    }

    private void ChangeOrde()
    {
        currentOrde++;
        if (currentOrde > ordes.Count)
        {
            Debug.Log("Fine Orde");
            var obstacles = FindObjectsOfType<ChaserObstacle>();
            foreach (var chaserObstacle in obstacles)
            {
                Destroy(chaserObstacle.gameObject);
            }

            var en = FindObjectsOfType<Enemy>();
            foreach (var enemy in en)
            {
                Destroy(enemy);
            }
            _currentsEnemies.Clear();
            OnCompleted?.Invoke();
            return;
        }
        ShowOrde();
    }

    private void OnDestroy()
    {
        foreach (var en in _currentsEnemies)
        {
            en.OnDeath -= OnDeath;
        }
        
        _currentsEnemies.Clear();
    }

    public void AddEnemy(Enemy enemy)
    {
        enemy.transform.SetParent(enemySpawn);
        _currentsEnemies.Add(enemy);
        enemy.OnDeath += OnDeath;
        _Viewer.ShowRemainEnemies(_currentsEnemies.Count);
    }
}
}
