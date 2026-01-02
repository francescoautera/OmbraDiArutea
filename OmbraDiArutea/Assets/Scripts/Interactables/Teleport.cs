using UnityEngine;

namespace OmbreDiAretua
{
    public class Teleport : AutoInteractable
    {
        
        [SerializeField] private string nameScene;
        public override void Execute()
        {
            var player = FindFirstObjectByType<Player>();
            player.HideAllUI();
            player.StopAll();
            FindFirstObjectByType<LevelManager>().LoadLevelScene(nameScene);
        }

        public override void Close()
        {
        
        }
    }
}