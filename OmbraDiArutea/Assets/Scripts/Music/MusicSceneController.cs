using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class MusicSceneController : MonoBehaviour
    {
        [SerializeField] private MusicData _musicData;
        [SerializeField] private bool playMusicOnStart = true;
        private IEnumerator Start()
        {
            if (!playMusicOnStart)
            {
                yield break;
                
            }
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