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
                levelController.OnCompletedLevel += OnCompleted;
                levelController.Setup(isCompleted[scene]);
            });
        }

        private void OnCompleted(string obj)
        {
            FindFirstObjectByType<LevelController>().OnCompletedLevel = null;
            isCompleted[obj] = true;
        }
    }
}