using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OmbreDiAretua
{
    public class SceneLoader : MonoBehaviour
    {
        private IEnumerator Start()
        {
            var fadeInitial = GetComponent<CanvasGroup>();
            var t = 0f;
            while (t < 1)
            {
                fadeInitial.alpha = Mathf.Lerp(1,0, t / 1);
                t += Time.deltaTime;
                yield return null;
            }
        }

        public void LoadScene(string sceneToLoad, string _sceneToRemove, Action OnEnd)
        {
            StartCoroutine(LoadCor());

            IEnumerator LoadCor()
            {

                var fadeInitial = GetComponent<CanvasGroup>();
                var t = 0f;
                while (t < 1)
                {
                    fadeInitial.alpha = Mathf.Lerp(0, 1, t / 1);
                    t += Time.deltaTime;
                    yield return null;
                }

                yield return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
                yield return SceneManager.UnloadSceneAsync(_sceneToRemove);

                t = 0f;
                
                while (t < 1)
                {
                    fadeInitial.alpha = Mathf.Lerp(1,0, t / 1);
                    t += Time.deltaTime;
                    yield return null;
                }
                OnEnd?.Invoke();
            }

        }
    }
}