using System.Collections.Generic;

using UnityEngine;

namespace OmbreDiAretua
{

    [CreateAssetMenu(menuName = "Dialogue/Line", fileName = "Line", order = 0)]
    public class Line : ScriptableObject
    {
        public SpeakerData currentSpeaker;
        [TextArea(10, 10)] public string line;
        public List<Line> nextLines = new List<Line>();
        public bool isMultichoice;


        public Line GetNextLine(int index =0)
        {
            if (nextLines.Count == 0 )
            {
                return null;
            }

            if (index >= nextLines.Count)
            {
                return nextLines[0];
            }

            return nextLines[index];
        }
#if UNITY_EDITOR
        void OnValidate()
        {
            if (!isMultichoice && nextLines.Count >0)
            {
                var nextLine = nextLines[0];
                nextLines.Clear();
                nextLines.Add(nextLine);
                return;
            }
            
        }
#endif
    }
}
