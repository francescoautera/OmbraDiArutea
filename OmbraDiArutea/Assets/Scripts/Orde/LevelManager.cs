using AYellowpaper.SerializedCollections;
using EasyButtons;
using UnityEngine;

namespace OmbreDiAretua
{
    public class LevelManager : MonoBehaviour
    {

        public SerializedDictionary<string,bool> isCompleted;
        public string hubScene;

        [Button]
        public void LoadLevelScene(string scene)
        {
            FindObjectOfType<SceneLoader>().LoadScene(scene,hubScene, () => { 
                var levelController = FindFirstObjectByType<LevelController>();
                if (levelController)
                {
                    levelController.OnCompletedLevel += OnCompleted;
                    levelController.Setup(isCompleted[scene]);
                }
            });
        }

        public void ReturnToMenu(string sceneToUnload)
        {
            FindFirstObjectByType<SceneLoader>().LoadScene("MainMenu",sceneToUnload,null);
            foreach (var value in isCompleted.Keys)
            {
                isCompleted[value] = false;
            }
        }



        public void Reload(string scene)
        {
            FindObjectOfType<SceneLoader>().LoadScene(scene,scene, () => { 
                var levelController = FindFirstObjectByType<LevelController>();
                if (!levelController)
                {
                    return;
                }
                levelController.OnCompletedLevel += OnCompleted;
                levelController.Setup(isCompleted[scene]);
            });
        }


        public void LoadHub(string scene)
        {
            FindObjectOfType<SceneLoader>().LoadScene(hubScene,scene,null);
        }
        
        

        private void OnCompleted(string obj)
        {
            FindFirstObjectByType<LevelController>().OnCompletedLevel = null;
            isCompleted[obj] = true;
        }

        public bool IsWorldCompleted(string scene)
        {
            return isCompleted[scene];
        }
    }
}