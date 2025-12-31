using UnityEngine;
using UnityEngine.Events;

namespace CrashDetective
{
    public class DialogueData : MonoBehaviour
    {
        public UnityEvent OnStartDialogue;
        public UnityEvent OnEndDialogue;
        [SerializeField] Line firstLine;
        private Line currentLine;
        private DialogueManager _dialogueManager;
    
        private void Start()
        {
            currentLine = firstLine;
            _dialogueManager = FindObjectOfType<DialogueManager>();
        }

        public void RequestStartDialogue()
        {
            Debug.Log("start");
            _dialogueManager.StartDialogue(this);
        }

        public Line TryGetNextLine(int index =0)
        {
            var line = currentLine.GetNextLine(index);
            if (line == null)
            {
                OnEndDialogue?.Invoke();
                return null;
            }
            currentLine = line;
            return line;
        }
        

        public Line GetFistLine()
        {
            return firstLine;
        }

        
    }
}
