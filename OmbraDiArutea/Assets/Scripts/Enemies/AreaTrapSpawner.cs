using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class AreaTrapSpawner : MonoBehaviour
    {
        [SerializeField] Transform _rockTransform;
        [SerializeField] float timerFeedback;
        [SerializeField] int scale;
        [SerializeField] float explosionRadius;
        [SerializeField] LayerMask _enemyLayer;
        [SerializeField] int damage;
        [SerializeField] DamageShower _damageShower;
        [Header("Animator")] 
        [SerializeField] Animator _animatorRock;

        public void ShowTrapFeedback()
        {
            StartCoroutine(ShowFeedbackCor());
        }

        private IEnumerator ShowFeedbackCor()
        {
            Vector3 maxScale = Vector3.one * scale;
            float t = 0f;
            while (t < timerFeedback)
            {
                _rockTransform.localScale = Vector3.Lerp(Vector3.zero,maxScale,t/timerFeedback);
                t += Time.deltaTime;
                yield return null;
            }
            _animatorRock.enabled = true;
            Explode();
        }

        private void Explode()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                transform.position,
                explosionRadius,
                _enemyLayer
            );

            foreach (var hit in hits)
            {
                Player enemy = hit.GetComponent<Player>();
                if (!enemy) continue;
                if (enemy.IsInvincibily)
                {
                    return;
                }
                int finalDamage = damage;
                var damageShower = Instantiate(
                    _damageShower,
                    enemy.transform.position,
                    Quaternion.identity
                );
                damageShower.ShowDamage( enemy.TakeDamage(finalDamage));
            }
        }

        public void OnEndAnimation()
        { 
            Destroy(gameObject);
        }

    }
}