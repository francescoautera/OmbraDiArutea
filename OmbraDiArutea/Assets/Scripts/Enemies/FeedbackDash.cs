using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace OmbreDiAretua
{
    public class FeedbackDash : MonoBehaviour
    {
        [SerializeField] private RectTransform _feedbackDash;
        [SerializeField] private Image _imageFeedback;
        private float timerDash;

        public void Init(float distance,float rotationZ,bool isInverted,float timerDash)
        {
            this.timerDash = timerDash;
            _feedbackDash.sizeDelta = new Vector2(distance*100, _feedbackDash.sizeDelta.y);
            _feedbackDash.rotation = Quaternion.Euler(0, 0, rotationZ);
           // _feedbackDash.localScale = new Vector3(isInverted ? -1 : 1, _feedbackDash.localScale.y);
            StartCoroutine(FeedbackDashCor());
        }

        IEnumerator FeedbackDashCor()
        {
            float t = 0f;
            while (t< timerDash)
            {
                _imageFeedback.fillAmount = Mathf.Lerp(0, 1, t / timerDash);
                t += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}