using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class MusicSceneController : MonoBehaviour
    {
        [SerializeField] private MusicData _musicData;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.3f);
            RequestPlayMusic();
        }

        public void RequestPlayMusic()
        {
            MusicManager.Instance.PlayMusic(_musicData);
        }


        public void RequestStopMusic(bool useFade)
        {
            MusicManager.Instance.StopMusic(useFade,new MusicData());
        }
    }
}