using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    
public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string nameSceneToUnload;

    public void StartGame()
    {
        FindFirstObjectByType<SceneLoader>().LoadScene(sceneToLoad,nameSceneToUnload,null);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
}
