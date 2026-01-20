using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
   
public class AretuaSpell : MonoBehaviour
{
   [SerializeField] Transform center;
   [SerializeField] List<EnemySpellBehaviour> _enemySpellBehaviours = new List<EnemySpellBehaviour>();
   [SerializeField] SpellStat _spellStat;
   [SerializeField] int damage;
   public void Execute()
   {
      foreach (var enemySpellBehaviour in _enemySpellBehaviours)
      {
         
         enemySpellBehaviour.Initialize(_spellStat,center.position,damage);
      }
   }
}
}
