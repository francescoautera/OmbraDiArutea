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
    }
    
    public float raggio = 5f;

    void Spawn(Enemy prefab)
    {
        Vector2 puntoRandom = Random.insideUnitCircle * raggio;
        Vector3 posizione = new Vector3(puntoRandom.x,puntoRandom.y,0);
        var instanceFeedback = Instantiate(_FeedbackSpawn, posizione, Quaternion.identity);
        
        instanceFeedback.Init(() =>
        {
            var enemy = Instantiate(prefab, posizione, Quaternion.identity);
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
            StartCoroutine(Wait());
            IEnumerator Wait()
            {
                yield return new WaitForSeconds(1.2f);
                if (TryCheckOrde())
                {
                    ChangeOrde();
                    yield break;
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
            OnCompleted?.Invoke();
            return;
        }
        ShowOrde();
    }
}
}
