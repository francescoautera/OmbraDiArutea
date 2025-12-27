using UnityEngine;
using UnityEngine.UI;

namespace OmbreDiAretua
{
    public class EnemyViewer : MonoBehaviour
    {
        public Image lifeImage;
        public Transform enemySprite;
        private int currentHealth;
        private Transform playerPosition;
        
        public void Setup(int health)
        {
            playerPosition = FindFirstObjectByType<Player>().transform;
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
            var remainHealth = currentHealth - damage;
            lifeImage.fillAmount = remainHealth / (float)currentHealth;
        }
    }
}