using UnityEngine;

namespace OmbreDiAretua
{
    public class FtueInvoker : MonoBehaviour
    {
        public FTUEEVENTS invokeFtue;

        public void Execute()
        {
            var ftueController =FindFirstObjectByType<FtueController>();
            ftueController.ShowEvent(invokeFtue);
        }
    }
}