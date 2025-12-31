using System;
using UnityEngine;

namespace OmbreDiAretua
{
    public class FeedbackSpawn : MonoBehaviour
    {
        private Action OnEnd;

        public void Init(Action OnEnd)
        {
            this.OnEnd = OnEnd;
        }

        public void Execute()
        {
            OnEnd?.Invoke();
            Destroy(gameObject);
        }
    }
}