using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{


    public class DeathController : MonoBehaviour
    {
        public string sceneToLoad;
        public CanvasGroupController _CanvasGroupController;

        private void Start()
        {
            GameGlobalEvents.OnPlayerDeath += OnPlayerDeath;
        }

        private void OnDestroy()
        {
            GameGlobalEvents.OnPlayerDeath -= OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            _CanvasGroupController.Show(null);
        }


        public void Retry()
        {
           FindObjectOfType<LevelManager>().Reload(sceneToLoad); 
        }

        public void LoadHub()
        {
            FindObjectOfType<LevelManager>().LoadHub(sceneToLoad); 

        }
    }
}