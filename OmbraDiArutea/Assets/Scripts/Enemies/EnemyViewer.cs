using UnityEngine;
using UnityEngine.UI;

namespace OmbreDiAretua
{
    public class EnemyViewer : MonoBehaviour
    {
        public Image lifeImage;
        public Transform enemySprite;
        public GameObject burnIcon;
        public GameObject slowIcon;
        private int currentHealth;
        private int maxHealth;
        private Transform playerPosition;
        
        public void Setup(int health)
        {
            playerPosition = FindFirstObjectByType<Player>().transform;
            maxHealth = health;
            currentHealth = health;
            lifeImage.fillAmount = 1;
            if (playerPosition.position.x < transform.position.x)
            {
                enemySprite.localScale = new Vector3(-(Mathf.Abs(enemySprite.localScale.x)), enemySprite.localScale.y,enemySprite.localScale.z);
            }
            else
            {
                enemySprite.localScale = new Vector3((Mathf.Abs(enemySprite.localScale.x)), enemySprite.localScale.y,enemySprite.localScale.z);

            }
        }

        private void Update()
        {
            if (playerPosition.position.x < transform.position.x)
            {
                enemySprite.localScale = new Vector3(-(Mathf.Abs(enemySprite.localScale.x)), enemySprite.localScale.y,enemySprite.localScale.z);
            }
            else
            {
                enemySprite.localScale = new Vector3((Mathf.Abs(enemySprite.localScale.x)), enemySprite.localScale.y,enemySprite.localScale.z);

            }
            
        }

        public void ShowDamage(int damage)
        {
            currentHealth -= damage;
            lifeImage.fillAmount = currentHealth / (float)maxHealth;
        }

        public void UpdateFire(bool isActive) => burnIcon.gameObject.SetActive(isActive);
        
        public void UpdateSlow(bool isActive) => slowIcon.gameObject.SetActive(isActive);
    }
}