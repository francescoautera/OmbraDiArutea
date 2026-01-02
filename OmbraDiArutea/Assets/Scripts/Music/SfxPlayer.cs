using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    public class SfxPlayer : MonoBehaviour
    {
        [SerializeField] internal MusicData _musicData;
        
        public virtual void PlayFx()
        {
            MusicManager.Instance.PlaySfx(_musicData);
        }
    }
}