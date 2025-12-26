using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace OmbreDiAretua
{
    [CreateAssetMenu(menuName = "Data/SpellData",fileName = "SpellData_")]
    public class SpellData : ScriptableObject
    {
        public SerializedDictionary<int, SpellStat> SpellStats = new SerializedDictionary<int, SpellStat>();
        public string nameSpell;
        public Sprite spriteSpell;
        public int currentLevel = 1;
        public GameObject instanceSpell;

        public SpellStat GetSpellStat() => SpellStats[currentLevel];
        public void IncreaseSpellLevel() => currentLevel++;
        public void ResetSpellLevel() => currentLevel = 1;
    }
}