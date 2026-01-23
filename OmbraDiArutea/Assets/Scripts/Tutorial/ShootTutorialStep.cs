using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class ShootTutorialStep : TutorialStep
    {
        public DialogueData DialogueData;
        private bool RequestShoot;
        public float timerWaitChange = 1.2f;
        [SerializeField] FtueInvoker _ftueInvoker;
    
        public override void InitTutorialStep()
        {
            DialogueData.RequestStartDialogue();
        }

        public override void UnlcokStep()
        {
            RequestShoot = true;
            _ftueInvoker.Execute();
        }

        private void Update()
        {
            if (!RequestShoot)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                RequestShoot = false;
                StartCoroutine(Wait());
            }
        
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(timerWaitChange);
            OnEndTutorialStep?.Invoke();
        }
    
    }
}