using UnityEngine;

namespace OmbreDiAretua
{
    public class EndingPanel : MonoBehaviour
    {
        [SerializeField] CanvasGroupController _canvasGroupController;
        [SerializeField] private Animator _animator;
        [SerializeField] private SfxPlayer _winSfx;
        [SerializeField] private string sceneName;

        public void ShowWin()
        {
            _canvasGroupController.Show(StartAnimation);
        }

        private void StartAnimation()
        {
            _winSfx.PlayFx();
            _animator.enabled = true;
        }

        public void ReturToHub()
        {
            FindFirstObjectByType<LevelManager>().ReturnToMenu(sceneName);
        }
    }
}