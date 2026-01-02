using System;
using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class CanvasGroupController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _group;
    
        public void Show(Action OnEnd)
        {
            StartCoroutine(ChangeAlpha(0, 1,OnEnd));
        }
        
        IEnumerator ChangeAlpha(float start, float end,Action OnEnd)
        {
            float t = 0f;
            while (t < 1f)
            {
                _group.alpha = Mathf.Lerp(start, end, t);
                t += Time.deltaTime;
                yield return null;
            }
            _group.alpha = end;
            OnEnd?.Invoke();
        } 

        public void Close(Action OnEnd)
        {
            StartCoroutine(ChangeAlpha(1, 0,OnEnd));
        }

    }
}