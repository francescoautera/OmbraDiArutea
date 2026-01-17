using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    
public class AretuaBossFightLogic : MonoBehaviour
{
    [SerializeField] DialogueData _startingDialogueData;
    [SerializeField] Enemy _boss;
    [SerializeField] CanvasGroupController _canvasGroupController;
    [SerializeField] EndingPanel _endingPanel;
    [SerializeField] private MusicSceneController startMusicFirstPhase;
    private bool canCheckLife;
    [Header("SecondPhase")] 
    [SerializeField] bool canInstance;
    [SerializeField] GameObject instanceObstacle;
    [SerializeField] float timerBtwnInstance;
    [SerializeField] MusicSceneController startMusicSecondPhase;
    [SerializeField] MusicSceneController startVictoryMusic;
    private float timerSpawn;

    private void Update()
    {
        if (!canInstance)
        {
            if (canCheckLife)
            {
                CheckLife();
            }
            return;    
        }
        
        
        timerSpawn += Time.deltaTime;
        if (timerSpawn > timerBtwnInstance)
        {
            timerSpawn = 0;
            var chaser = Instantiate(instanceObstacle, _boss.transform.position, Quaternion.identity);
            chaser.GetComponent<ChaserObstacle>().Init(FindFirstObjectByType<Player>());
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.4f);
        Init();
    }

    private void Init()
    {
        _startingDialogueData.RequestStartDialogue();
        canInstance = false;
        canCheckLife = false;
        _canvasGroupController.Close(null);
        _boss.OnDeath += OnDeath;
        GameGlobalEvents.OnPlayerDeath += StopBattle;
    }

    private void OnDeath(Enemy obj)
    {
        canInstance = false;
        OpenEndingPanel();
        _boss.OnDeath = null;
    }

    public void StartingBattle()
    {
        _boss.Init();
        canCheckLife = true;
        startMusicFirstPhase.RequestPlayMusic();
        _canvasGroupController.Show(null);
    }
    

    public void StartSecondPhase()
    {
        canInstance = true;
        timerSpawn = 0f;
        startMusicSecondPhase.RequestPlayMusic();
    }

    public void StopBattle()
    {
        GameGlobalEvents.OnPlayerDeath -= StopBattle;
        canInstance = false;
        timerSpawn = 0f;
        _boss.Stop();
    }

    private void OpenEndingPanel()
    {
        canInstance = false;
        timerSpawn = 0f;
        startVictoryMusic.RequestPlayMusic();
    }

    private void CheckLife()
    {
        var isLifeMoreThanPercentageLife = _boss.IsRemainPercentageLife(0.5f);
        if (isLifeMoreThanPercentageLife)
        {
            return;
        }
        StartSecondPhase();
    }
}
}
