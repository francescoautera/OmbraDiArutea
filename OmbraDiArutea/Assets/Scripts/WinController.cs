using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    
public class WinController : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] CanvasGroupController _canvasGroupController;
    [SerializeField] private Animator _animator;

    public void ShowWin()
    {
        _canvasGroupController.Show(StartAnimation);
    }

    private void StartAnimation()
    {
        _animator.enabled = true;
    }

    public void ReturToHub()
    {
        FindFirstObjectByType<LevelManager>().LoadHub(sceneName);
    }

}
}
