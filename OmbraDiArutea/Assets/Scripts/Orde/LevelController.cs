using System;
using UnityEngine;

namespace OmbreDiAretua
{
    public class LevelController : MonoBehaviour
    {
        public bool isCompleted;
        public string nameLevel;
        public Action<string> OnCompletedLevel;
        [SerializeField] OrdeController _ordeController;
        [SerializeField] Player _player;

        public void Setup(bool isCompleted)
        {
            this.isCompleted = isCompleted;
            _ordeController.ShowOrde();
            _ordeController.OnCompleted += OnCompleted;
            _player.Init();
        }

        private void OnCompleted()
        {
            OnCompletedLevel?.Invoke(nameLevel);
        }
    }
}