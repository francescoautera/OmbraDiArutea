using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace OmbreDiAretua
{
    public class FtueViewer : MonoBehaviour
    {
        [SerializeField] private CanvasGroupController _groupController;
        [SerializeField] private TextMeshProUGUI eventFtue;

        public void Show(FtueData ftueData,Action OnEnd)
        {
            eventFtue.text = ftueData.description;
            _groupController.Show(() =>
            {
                StartCoroutine(Wait(ftueData.timerAppear, () =>
                {
                    _groupController.Close(OnEnd);   
                }));

            });
            
        }

        IEnumerator Wait(float timerWait,Action OnEnd)
        {
            yield return new WaitForSeconds(timerWait);
            OnEnd?.Invoke();
        }

    }
}