using System;
using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class PowerUpAnimationController : MonoBehaviour
    {
        [SerializeField] private Transform endTransform;
        [SerializeField] private float timerAnimation;
        [SerializeField] private AnimationCurve _curveAnimation;
        [SerializeField] private Transform objectToMove;
        private Action OnEnd;
        

        public void Move(Action OnEnd, Transform initTransform)
        {
            this.OnEnd = OnEnd;
            StartCoroutine(MoveCor(initTransform));

        }

        private IEnumerator MoveCor(Transform initTransform)
        {
            
            float t = 0f;
            objectToMove.position = initTransform.position;
            objectToMove.localScale = Vector3.one;
            while (t < timerAnimation)
            {
                objectToMove.position = Vector3.Lerp(initTransform.position,endTransform.position,_curveAnimation.Evaluate(t/timerAnimation));
                t += Time.deltaTime;
                yield return null;
            }
            objectToMove.position = endTransform.position;
            objectToMove.localScale = Vector3.zero;
            OnEnd?.Invoke();
        }
    }
}