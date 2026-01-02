using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{
    public class ShowFtueInteractable : AutoInteractable
    {
        [SerializeField] private List<FtueInvoker> _ftueInvoker = new List<FtueInvoker>();
        
        
        public override void Execute()
        {
            foreach (var ftueInvoker in _ftueInvoker)
            {
                ftueInvoker.Execute();
            }
        }

        public override void Close()
        {
            
        }
    }
}