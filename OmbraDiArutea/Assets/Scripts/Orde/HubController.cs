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
            var levelManager = FindFirstObjectByType<LevelManager>();
            foreach (var values in levelManager.isCompleted.Values)
            {
                if (!values)
                {
                    yield break;
                }
            }
            
            _openTeleport.Unlock();
        }
    }
}