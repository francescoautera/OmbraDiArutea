using System.Collections;
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
        public CanvasGroup panel;
        private SpellContainerData _currentSpell;
        [Header("Spell")] 
        [SerializeField] private SfxPlayer _shoot;
        [SerializeField] private SfxPlayer reloading;

        [Header("feedback")] 
        [SerializeField] GameObject _feedback;
        private int reduceDamage = 0;
        
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
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z; // distanza camera → mondo
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
            var spellStat = _currentSpell.SpellData.GetSpellStat();
            spellStat.damage -= reduceDamage;
            _currentSpell.SpellController.SetInCooldown(spellStat);
            var instanceSpell = Instantiate(_currentSpell.SpellData.instanceSpell,ShootTransform.transform.position,Quaternion.identity);
            instanceSpell.GetComponent<SpellBehaviour>().Initialize(spellStat,mouseWorldPos,_currentPlayer.force);
            _shoot.PlayFx();
        }

        public override void BlockMechanic()
        {
            blockMechanics = true;
        }

        public override void UnblockMechanic()
        {
            blockMechanics = false;
        }

        public override void HideAllUI()
        {
            
            StartCoroutine(ChangeAlpha(1, 0));
        }

        public override void ShowAllUI()
        {
            StartCoroutine(ChangeAlpha(0, 1));
        }
        
         IEnumerator ChangeAlpha(float start, float end)
        {
            float t = 0f;
            while (t < 1f)
            {
                panel.alpha = Mathf.Lerp(start, end, t/1f);
                t += Time.deltaTime;
                yield return null;
            }

            panel.alpha = end;
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
                    reloading.PlayFx();
                    return;
                }
                
                Shoot();
            }
        }

        private bool isReduced = false;
        
        public void ReduceDamage(float timer, int reduce)
        {
            if (isReduced)
            {
                return;
            }

            isReduced = true;
            reduceDamage = reduce;
            _feedback.SetActive(true);
            StartCoroutine(ReduceDamageCor(timer));
        }


        IEnumerator ReduceDamageCor(float timer)
        {
            yield return new WaitForSeconds(timer);
            reduceDamage = 0;
            isReduced = false;
            _feedback.SetActive(false);
        }
    }
    
    
}