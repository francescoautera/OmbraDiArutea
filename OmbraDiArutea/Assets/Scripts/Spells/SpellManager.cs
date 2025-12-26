using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using EasyButtons;
using UnityEngine;

namespace OmbreDiAretua
{
    public class SpellManager : PlayerMechanics
    {
        private bool blockMechanics;
        public List<SpellContainerData> spellContainerDatas = new();
        public Transform ShootTransform;
        private SpellContainerData _currentSpell;

        public override void Init(PlayerData playerData)
        {
            base.Init(playerData);
            foreach (var spellContainerData in spellContainerDatas)
            {
                spellContainerData.SpellController.Setup(spellContainerData.SpellData);
                spellContainerData.SpellController.OnChangeSpellRequest += OnChangeSpellRequest;
            }
            _currentSpell = spellContainerDatas[0];
            _currentSpell.SpellController.Selected();
        }

        private void OnChangeSpellRequest(SpellController spellController)
        {
            for (var index = 0; index < spellContainerDatas.Count; index++)
            {
                var spellContainer = spellContainerDatas[index];
                if (spellContainer.SpellController == spellController)
                {
                    if (_currentSpell == spellContainer)
                    {
                        return;
                    }
                    ChangeSpell(index);
                }
            }
        }

        public void ChangeSpell(int index)
        {
            
            if (_currentSpell != null)
            {
                _currentSpell.SpellController.UnSelected();
            }
            
            _currentSpell = spellContainerDatas[index];
            _currentSpell.SpellController.Selected();
        }

        [Button]
        public void IncreaseLevelSpell(SpellData spellData)
        {
            foreach (var spellContainerData in spellContainerDatas)
            {
                if (spellData.nameSpell == spellContainerData.SpellData.nameSpell)
                {
                    spellData.IncreaseSpellLevel();
                    spellContainerData.SpellController.Setup(spellContainerData.SpellData);
                    return;
                }
            }
        }

        public void ResetAllLevels()
        {
            foreach (var spellContainer in spellContainerDatas)
            {
                spellContainer.SpellData.ResetSpellLevel();
                spellContainer.SpellController.Setup(spellContainer.SpellData);
            }
        }

        public void Shoot()
        {
            var spellStat = _currentSpell.SpellData.GetSpellStat();
            _currentSpell.SpellController.SetInCooldown(spellStat);
            var instanceSpell = Instantiate(_currentSpell.SpellData.instanceSpell,ShootTransform.transform.position,Quaternion.identity);
        }

        public override void BlockMechanic()
        {
            blockMechanics = true;
        }

        public override void UnblockMechanic()
        {
            blockMechanics = false;
        }

        private void Update()
        {
            if (blockMechanics)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (_currentSpell.SpellController.IsInCooldown)
                {
                    return;
                }
                Shoot();
            }
        }
    }
    
}