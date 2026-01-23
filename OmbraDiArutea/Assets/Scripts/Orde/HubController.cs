using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class HubController : MonoBehaviour
    {
        [SerializeField] private Teleport _openTeleport;


        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.3f);
            var playerShooter = FindObjectOfType<SpellManager>();
            playerShooter.ResetAllLevels();
            var levelManager = FindFirstObjectByType<LevelManager>();
            foreach (var values in levelManager.isCompleted)
            {
                if (values.Key == "FinalBoss")
                {
                    continue;
                }
                
                if (!values.Value)
                {
                    yield break;
                }
            }
            
            _openTeleport.Unlock();
        }
    }
}