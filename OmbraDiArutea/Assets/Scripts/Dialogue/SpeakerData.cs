using UnityEngine;

namespace OmbreDiAretua
{
    [CreateAssetMenu(menuName = "Dialogue/Speaker", fileName = "Speaker", order = 0)]
    public class SpeakerData : ScriptableObject
    {
        public string nameSpeaker;
        public Sprite speakerSprite;
    }
}