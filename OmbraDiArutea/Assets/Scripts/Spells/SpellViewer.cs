using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OmbreDiAretua
{
    public class SpellViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelSpell;
        [SerializeField] private Image cooldownFilledImage;
        [SerializeField] private Image selectedImage;

        public void Setup(SpellData spellData)
        {
            _levelSpell.text = "Lv" + spellData.currentLevel;
            cooldownFilledImage.fillAmount = 0;
        }

        public void ResetCooldown(float cooldown,Action OnEnd)
        {
            cooldownFilledImage.fillAmount = 1;
            StartCoroutine(FillImage(cooldown,OnEnd));

        }

        private IEnumerator FillImage(float cooldown,Action OnEnd)
        {
            float t = 0f;
            while (t < cooldown)
            {
                cooldownFilledImage.fillAmount = Mathf.Lerp(1, 0, t / cooldown);
                t += Time.deltaTime;
                yield return null;
            }
            cooldownFilledImage.fillAmount = 0;
            OnEnd?.Invoke();
        }

        public void Selected()
        {
            selectedImage.enabled = true;
        }

        public void UnSelected()
        {
            selectedImage.enabled = false;
        }
    }
}