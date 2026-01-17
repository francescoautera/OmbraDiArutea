using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    
public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string nameSceneToUnload;
    [SerializeField] private string tutorialtoLoad;

   

    public void StartGame()
    {
        FindFirstObjectByType<SceneLoader>().LoadScene(sceneToLoad,nameSceneToUnload,null);
    }
    
    public void StartTutorial()
    {
        FindFirstObjectByType<SceneLoader>().LoadScene(tutorialtoLoad,nameSceneToUnload,null);
    }
    

    public void QuitGame()
    {
        Application.Quit();
    }

}
}
