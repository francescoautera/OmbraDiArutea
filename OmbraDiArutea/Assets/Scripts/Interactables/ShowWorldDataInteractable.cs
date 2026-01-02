using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace OmbreDiAretua
{
    public class ShowWorldDataInteractable : AutoInteractable
    {
        [SerializeField] private CanvasGroup _group;
        [SerializeField] GameObject isCompleted;
        [SerializeField] string world;
    
        public override void Execute()
        {
            bool isWorldCompleted = FindObjectOfType<LevelManager>().IsWorldCompleted(world);
            isCompleted.SetActive(isWorldCompleted);
            StartCoroutine(ChangeAlpha(0, 1));
        }
        
        IEnumerator ChangeAlpha(float start, float end)
        {
            float t = 0f;
            while (t < 1f)
            {
                _group.alpha = Mathf.Lerp(start, end, t);
                t += Time.deltaTime;
                yield return null;
            }

            _group.alpha = end;
        } 

        public override void Close()
        {
            StartCoroutine(ChangeAlpha(1, 0));
        }
    }
}