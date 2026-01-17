using System;
using UnityEngine;

namespace OmbreDiAretua
{
   public class DialogueManager : MonoBehaviour
   {
      public static Action OnStartDialogue;
      public static Action OnEndDialogue;
      private DialogueData _currentDialogue;
      [SerializeField] DialogueViewer _viewer;
      

     

      public void StartDialogue(DialogueData data)
      {
         data.OnStartDialogue.RemoveAllListeners();
         data.OnStartDialogue?.Invoke();
         _currentDialogue = data;
         OnStartDialogue?.Invoke();
         _currentDialogue.OnEndDialogue.RemoveAllListeners();
         _currentDialogue.OnEndDialogue.AddListener(EndDialogue);
         var line = _currentDialogue.GetFistLine();
         _viewer.Setup(line,OnNextRequested);
      }

      private void OnDestroy()
      {
         OnStartDialogue = null;
         OnEndDialogue = null;
      }

      private void OnNextRequested()
      {
         var line = _currentDialogue.TryGetNextLine();
         if (line == null)
         {
            EndDialogue();
            return;
         }

         if (line.isMultichoice)
         {
            _viewer.SetupMultichoice(line,OnNextRequested);
            return;
         }
         _viewer.Setup(line,OnNextRequested);
      }

      private void OnNextRequested(int index)
      {
         var line = _currentDialogue.TryGetNextLine(index);
         if (line == null)
         {
            EndDialogue();
            return;
         }

         if (line.isMultichoice)
         {
            _viewer.SetupMultichoice(line,OnNextRequested);
            return;
         }
         _viewer.Setup(line,OnNextRequested);
      }

      private void EndDialogue()
      {
         _viewer.Close(); 
         _currentDialogue?.OnEndDialogue.RemoveAllListeners();
         _currentDialogue = null;
         OnEndDialogue?.Invoke();
      }
   }
}
