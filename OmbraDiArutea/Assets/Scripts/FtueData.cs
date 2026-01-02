using UnityEngine;

namespace OmbreDiAretua
{
    [CreateAssetMenu(menuName = "Data/FtueData", fileName = "FtueData", order = 0)]
    public class FtueData : ScriptableObject
    {
        [TextArea(3,3)]public string description;
        [Range(1,5)]public float timerAppear;
    }
}