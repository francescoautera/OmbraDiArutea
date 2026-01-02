using System;
using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private float defaultFadeTimer;
    [SerializeField,Range(0,1)] private float multiplierVolume = 1; 
    [SerializeField,Range(0,1)] private float multiplierSfxVolume = 1;
    MusicData _currentMusicData;

    public float MultiplierVolume => multiplierVolume;
    public float MultiplierSFX => multiplierSfxVolume;

    private void Start()
    {
        if (Instance == null)
        {
            
            Instance = this;
        }
    }

    public void PlayMusic(MusicData musicData)
    {
        _currentMusicData = musicData;
        StartCoroutine(FadeMusicCoroutine(_musicSource, musicData));
    }

    public void StopMusic(bool useFade,MusicData musicData)
    {
        if (useFade)
        {
            StartCoroutine(FadeMusicCoroutine(_musicSource,musicData));
            return;
        }   
        _musicSource.Stop();
    }

    private IEnumerator FadeMusicCoroutine(AudioSource audioSource, MusicData musicDatas)
    {
        float startingVolume = audioSource.volume;
        float maxT = musicDatas != null && musicDatas.timerFade > 0 ? musicDatas.timerFade : defaultFadeTimer / 2;
        if (audioSource.clip == null)
        {
            maxT = 0;
        }
        float t = maxT;
        float a = 0;
    

        while (t > 0)
        {
            t -= Time.deltaTime;
            a = (t / maxT) * startingVolume * multiplierVolume;
            audioSource.volume = a;
            yield return null;
        }

        if (musicDatas == null || musicDatas.audioClip == null)
        {
            audioSource.clip = null;
            yield break;
        }
        
        maxT = musicDatas != null && musicDatas.timerFade > 0 ? musicDatas.timerFade : defaultFadeTimer / 2;
        audioSource.clip = musicDatas.audioClip;
        audioSource.loop = true;
        audioSource.Play();
        while (t < maxT)
        {
            t += Time.deltaTime;
            a = (t / maxT) * musicDatas.volume * multiplierVolume;
            audioSource.volume = a;
            yield return null;
        }
    }
    

    public void PlaySfx(MusicData musicData)
    {
        _sfxSource.PlayOneShot(musicData.audioClip,musicData.volume * multiplierSfxVolume);
    }

    public void StopSfx()
    {
        _sfxSource.Stop();
    }

    public void SetVolumeMusic(float value) => multiplierVolume = value;
    public void SetSfxVolume(float value) => multiplierSfxVolume = value;

    public void ApplyChange()
    {
        _musicSource.volume = 0.5f * multiplierVolume;
        _sfxSource.volume = 0.5f * multiplierSfxVolume;
    }
}

[Serializable]
public class MusicData
{
  public AudioClip audioClip;
  public float timerFade;
  public float volume = 0.5f;
}

}
