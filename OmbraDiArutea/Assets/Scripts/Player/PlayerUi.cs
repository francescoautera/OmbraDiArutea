using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OmbreDiAretua
{
    public class PlayerUi : MonoBehaviour
    {
        [SerializeField] Image _playerImage;
        [SerializeField] float timerFill = 0.3f;
        [SerializeField] AnimationCurve _timerCurve;
        [Header("stats")] 
        [SerializeField]  TextMeshProUGUI forceText;
        [SerializeField]  TextMeshProUGUI defenceText;
        [SerializeField]  TextMeshProUGUI speedText;
        int maxHealth;
        int currentHealth;
    

        public void Init(PlayerData playerData)
        {
            maxHealth = playerData.health;
            currentHealth = playerData.health;
            _playerImage.fillAmount = currentHealth / (float)maxHealth;
            forceText.text = playerData.force.ToString();
            defenceText.text = playerData.defence.ToString();
            speedText.text = Mathf.RoundToInt( playerData.speed).ToString(CultureInfo.InvariantCulture);
        }

        public void UpdateStats(PlayerData playerData)
        {
            forceText.text = playerData.force.ToString();
            defenceText.text = playerData.defence.ToString();
            speedText.text = Mathf.RoundToInt( playerData.speed).ToString(CultureInfo.InvariantCulture);
        }

        public void UpdateHealth(PlayerData playerData)
        {
            if (currentHealth < playerData.health)
            {
                currentHealth = playerData.health;
                if (maxHealth < playerData.health)
                {
                    maxHealth = currentHealth;
                }
                _playerImage.fillAmount = currentHealth / (float)maxHealth;
                return;
            }
            StopAllCoroutines();
            StartCoroutine(MoveFill(currentHealth, playerData.health));
        }

        private IEnumerator MoveFill(int currHealth, int reachHealth)
        {
            float t = 0f;
            while (t < timerFill)
            {
                float lerp = Mathf.Lerp(currHealth, reachHealth, _timerCurve.Evaluate(t / timerFill));
                _playerImage.fillAmount = lerp / maxHealth;
                t += Time.deltaTime;
                yield return null;
            }

            currentHealth = reachHealth;
            _playerImage.fillAmount = reachHealth / (float)maxHealth;
        }
    }
}