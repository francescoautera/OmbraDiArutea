using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class FtueDialogue : MonoBehaviour
    {
        public DialogueData DialogueData;
        public FTUEEVENTS Ftueevents = FTUEEVENTS.FirstDialogue;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            var ftueController =FindFirstObjectByType<FtueController>();
            if (ftueController.IsEventMaded(Ftueevents))
            {
                yield break;
            }
            DialogueData.RequestStartDialogue();
            ftueController.SetEventCompleted(Ftueevents);
        }
    }
}