using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OmbreDiAretua
{

 public enum levelPowerUp
 {
   Level1,
   Level2,
 }
 
 

 public class PowerUpController : MonoBehaviour
 {
     [SerializeField] SerializedDictionary<int,  List<PowerUpProbability>> _powerUpProbabilities = new SerializedDictionary<int, List<PowerUpProbability>>();
     [SerializeField] private List<SpellData> firstLevelSpellDatas = new List<SpellData>();
     [SerializeField] private PowerUpViewer _powerUpViewer;
     List<SpellData> _secondLevelSpellDatas = new List<SpellData>();
     private Action OnEnd;

     private void Start()
     {
         _powerUpViewer.OnSelected += OnSelected;
     }

     private void OnSelected(SpellData spellData)
     {
         FindFirstObjectByType<SpellManager>().IncreaseLevelSpell(spellData);
         OnEnd?.Invoke();
         FindObjectOfType<Player>().RestartAll();
         if (firstLevelSpellDatas.Contains(spellData))
         {
             firstLevelSpellDatas.Remove(spellData);
             _secondLevelSpellDatas.Add(spellData);
             return;
         }

         _secondLevelSpellDatas.Remove(spellData);
     }

     public void ShowPowerUp(int ordeNumber,Action OnEnd)
     {
         this.OnEnd = OnEnd;
         FindObjectOfType<Player>().StopAll();
         ListExtensions.Shuffle(firstLevelSpellDatas);
         ListExtensions.Shuffle(_secondLevelSpellDatas);
         
         List<PowerUpProbability> listProbability = _powerUpProbabilities[ordeNumber];
         var listPowerUpData = new List<PowerUpData>();
         int currentIndexSpellFirst = 0;
         int currentIndexSpellSecond = 0;
         for (var index = 0; index < listProbability.Count; index++)
         {
             var powerUpProbability = listProbability[index];
             var random = Random.Range(0, 1f);
             if (random <= powerUpProbability.percentageFirstLevel)
             {
                 var powerUpData = new PowerUpData()
                 {
                     SpellData = firstLevelSpellDatas[currentIndexSpellFirst],
                     level = 1
                 };
                 listPowerUpData.Add(powerUpData);
                 currentIndexSpellFirst++;
                 continue;
             }
             var powerUpDataSecondLevel = new PowerUpData()
             {
                 SpellData = _secondLevelSpellDatas[currentIndexSpellSecond],
                 level = 2
             };
             listPowerUpData.Add(powerUpDataSecondLevel);
             currentIndexSpellSecond++;
         }
         
         _powerUpViewer.Open(listPowerUpData);
     }

 }

 [Serializable]
public class PowerUpProbability
{
    public levelPowerUp firstLevel = levelPowerUp.Level1;
    [Range(0,1)] public float percentageFirstLevel;
}

[Serializable]
public class PowerUpData
{
    public SpellData SpellData;
    public int level;
}
}
