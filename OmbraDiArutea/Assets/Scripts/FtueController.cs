using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

namespace OmbreDiAretua
{
    public enum FTUEEVENTS
    {
        FirstDialogue,
        FirstMovement,
        FirstShoot,
        FirstOrde,
        FirstSpellChange,
        NONE
    }

    public class FtueController : MonoBehaviour
    { 
        [SerializeField] SerializedDictionary<FTUEEVENTS, FtueContaier> ftueEvents = new SerializedDictionary<FTUEEVENTS, FtueContaier>();
        [SerializeField] private FtueViewer _ftueViewer;
        
        private Queue<FTUEEVENTS> ftueQueue = new Queue<FTUEEVENTS>();
        private FTUEEVENTS _current = FTUEEVENTS.NONE;

        public bool IsEventMaded(FTUEEVENTS events)
        {
            return ftueEvents[events].isCompleted;
        }

        public void SetEventCompleted(FTUEEVENTS ftueevents)
        {
            ftueEvents[ftueevents].isCompleted = true;
        }

        public void ShowEvent(FTUEEVENTS ftueevents)
        {
            if (ftueEvents[ftueevents].isCompleted)
            {
                TryShowNext();
                return;
            }
            
            if (_current != FTUEEVENTS.NONE)
            {
                ftueQueue.Enqueue(ftueevents);
                return;
            }

            _current = ftueevents;
            ftueEvents[ftueevents].isCompleted = true;
            _ftueViewer.Show(ftueEvents[ftueevents].FtueData,TryShowNext);
        }

        public void TryShowNext()
        {
            
            _current = FTUEEVENTS.NONE;
            
            if (ftueQueue.Count == 0)
            {
                return;
            }
            ShowEvent(ftueQueue.Dequeue());
        }

    }

    [Serializable]
    public class FtueContaier
    {
        public bool isCompleted;
        public FtueData FtueData;
    }
}
