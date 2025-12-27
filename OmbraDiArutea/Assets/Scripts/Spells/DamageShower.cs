using TMPro;
using UnityEngine;

namespace OmbreDiAretua
{
    public class DamageShower : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _damageText;

        public void ShowDamage(int damage)
        {
            _damageText.text = damage.ToString();
        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }

    }
}