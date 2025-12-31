using System;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    public class PowerUpViewer : MonoBehaviour
    {
        public Action<SpellData> OnSelected;
        public GameObject panel;
        public List<PowerUpChoicer> PowerUpChoicers = new List<PowerUpChoicer>();
        public PowerUpAnimationController PowerUpAnimationController;
   
        public void Open(List<PowerUpData> spells)
        {
            panel.SetActive(true);
            for (var index = 0; index < PowerUpChoicers.Count; index++)
            {
                var powerUpChoicer = PowerUpChoicers[index];
                powerUpChoicer.OnClicked += OnClicked;
                powerUpChoicer.Init(spells[index].SpellData,spells[index].level);
            }
        }

        private SpellData _spellData;
        private void OnClicked(SpellData spellData, PowerUpChoicer choicer)
        {
            PowerUpAnimationController.Move(Close,choicer.transform);
            _spellData = spellData;
        
            foreach (var powerUpChoicer in PowerUpChoicers)
            {
                powerUpChoicer.OnClicked -= OnClicked;
                powerUpChoicer.DeInit();
            }
        }

        private void Close()
        {
            OnSelected?.Invoke(_spellData);
            panel.SetActive(false);
        
        }
    }
}