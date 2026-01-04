using UnityEngine;

namespace OmbreDiAretua
{
    public class Teleport : AutoInteractable
    {
        
        [SerializeField] private string nameScene;
        [SerializeField] bool isLock;
        [SerializeField] DialogueData _blockTeleport;
            
        public override void Execute()
        {
            if (isLock)
            {
                _blockTeleport.RequestStartDialogue();
                return;
            }
            var player = FindFirstObjectByType<Player>();
            player.HideAllUI();
            player.StopAll();
            FindFirstObjectByType<LevelManager>().LoadLevelScene(nameScene);
        }

        public override void Close()
        {
        
        }

        public void Unlock() => isLock = false;
    }
}