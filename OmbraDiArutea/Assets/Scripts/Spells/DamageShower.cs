using TMPro;
using UnityEngine;

namespace OmbreDiAretua
{
    public class DamageShower : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageText;
        [SerializeField] private TextMeshProUGUI _superEffectiveDamamgeText;

        public void ShowDamage(int damage,bool isSuperEffective = false)
        {
            _damageText.text = damage.ToString();
            _superEffectiveDamamgeText.text = damage.ToString();
            _superEffectiveDamamgeText.gameObject.SetActive(isSuperEffective);
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }

    }
}