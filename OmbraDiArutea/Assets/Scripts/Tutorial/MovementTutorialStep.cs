using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class MovementTutorialStep : TutorialStep
    {
        public DialogueData DialogueData;
        private bool RequestMovement;
        [SerializeField] FtueInvoker _ftueInvoker;
    
        public override void InitTutorialStep()
        {
            DialogueData.RequestStartDialogue();
        }

        public override void UnlcokStep()
        {
            RequestMovement = true;
            _ftueInvoker.Execute();
        }

        private void Update()
        {
            if (!RequestMovement)
            {
                return;
            }

            if (Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") != 0)
            {
                RequestMovement = false;
                StartCoroutine(Wait());
            }
        
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.3f);
            OnEndTutorialStep?.Invoke();
        }
    
    }
}