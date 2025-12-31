using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderMainScene : MonoBehaviour
{

    [SerializeField] private string mainScene;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (SceneManager.GetSceneByName(mainScene).name != mainScene)
        {
            yield return SceneManager.LoadSceneAsync(mainScene, LoadSceneMode.Additive);
               
        }
    } 
}
