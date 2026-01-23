using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class ChangeSpellTutorialStep : TutorialStep
    {
        public DialogueData DialogueData;
        public float timerWaitChange;
        private bool RequestShoot;
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

            var isPressed = Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
                            Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha3) ||
                            Input.GetKeyDown(KeyCode.Alpha5);
            if (isPressed)
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