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
    private int currentOrde = 1;
    private List<Enemy> _currentsEnemies = new List<Enemy>();

    private void Start()
    {
        _PowerUpController = FindObjectOfType<PowerUpController>();
        ShowOrde();
        
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
    }
    
    public float raggio = 5f;

    void Spawn(Enemy prefab)
    {
        Vector2 puntoRandom = Random.insideUnitCircle * raggio;
        Vector3 posizione = new Vector3(puntoRandom.x,puntoRandom.y,0);

        var enemy = Instantiate(prefab, posizione, Quaternion.identity);
        enemy.OnDeath += OnDeath;
        _currentsEnemies.Add(enemy);
    }

    private void OnDeath(Enemy enemy)
    {
        enemy.OnDeath -= OnDeath;
        _currentsEnemies.Remove(enemy);
        if (_currentsEnemies.Count <= 0)
        {
            _PowerUpController.ShowPowerUp(currentOrde,ChangeOrde);    
        }
    }

    private void ChangeOrde()
    {
        currentOrde++;
        if (currentOrde > ordes.Count)
        {
            Debug.Log("Fine Orde");
            return;
        }
        ShowOrde();
    }
}
}
