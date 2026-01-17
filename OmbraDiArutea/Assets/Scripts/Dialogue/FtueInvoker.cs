using UnityEngine;

namespace OmbreDiAretua
{
    public class FtueInvoker : MonoBehaviour
    {
        public FTUEEVENTS invokeFtue;

        public void Execute()
        {
            var ftueController =FindObjectOfType<FtueController>();
            ftueController.ShowEvent(invokeFtue);
        }
    }
}