using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OmbreDiAretua
{
    
    public class DialogueViewer : MonoBehaviour
    {
        public GameObject panel;
        [SerializeField] Button onNext;
        [SerializeField] Button onSkip;

        [Header("DialogueComponents")] 
        [SerializeField] private GameObject bodyContainer;
        [SerializeField] private TextMeshProUGUI bodyText;
        [SerializeField] private TextMeshProUGUI speakerText;
        [SerializeField] private Image speakerImage;

        [Header("TextComponents")] 
        [SerializeField] private float timerWrite;
        [SerializeField] private float timerCharXChar;
        [SerializeField] private float timerSkip;
        [SerializeField] private bool immediateText;
        [SerializeField] private bool printXChar;

        [Header("MultiChoice")] 
        [SerializeField] private GameObject panelMultichoice;
        [SerializeField] private List<ButtonMultiChoice> _buttonMultiChoices;
        
        private string _currentString;

        private void Start()
        {
            onSkip.onClick.AddListener(ActiveSkip);
            panel.SetActive(false);
        }

        public void Setup(Line line,Action OnClickEvent)
        {
            panel.SetActive(true);
            bodyContainer.SetActive(true);
            panelMultichoice.SetActive(false);
            _currentString = line.line;
            bodyText.gameObject.SetActive(true);
            bodyText.text = "";
            speakerText.text = line.currentSpeaker.nameSpeaker;
            speakerImage.sprite = line.currentSpeaker.speakerSprite;
            onNext.onClick.RemoveAllListeners();
            onNext.onClick.AddListener(()=>OnClickEvent?.Invoke());
            onNext.gameObject.SetActive(false);
            onSkip.enabled = !immediateText;
            StartCoroutine(WriteDialogue(_currentString, _currentString,printXChar ? timerCharXChar : timerWrite));
        }

        public void SetupMultichoice(Line line, Action<int> OnChoice)
        {
            panel.SetActive(true);
            panelMultichoice.SetActive(true);
            bodyContainer.SetActive(false);
            onSkip.enabled = false;
            speakerText.text = line.currentSpeaker.nameSpeaker;
            speakerImage.enabled = line.currentSpeaker.speakerSprite != null;
            speakerImage.sprite = line.currentSpeaker.speakerSprite;
       
        }

        IEnumerator WriteDialogue(string stringToRead,string fullString,float waitTimer)
        {
            var stringSplitted = stringToRead.Split(' ');
            int index = 0;
            while (index < stringSplitted.Length)
            {
                if (printXChar)
                {
                    var stringValue = stringSplitted[index].ToCharArray();
                    foreach (var chars in stringValue)
                    {
                        bodyText.text += chars.ToString();
                        yield return new WaitForSeconds(waitTimer);
                    }
                    bodyText.text += " ";
                }
                else
                {
                    bodyText.text += stringSplitted[index] + " ";
                    yield return new WaitForSeconds(waitTimer);
                }
                index++;
               
            }

            bodyText.text = fullString;
            onNext.gameObject.SetActive(true);
        }

        private void ActiveSkip()
        {
            onSkip.enabled = false;
            StopAllCoroutines();
            var splittedString = _currentString.Replace(bodyText.text, "", StringComparison.OrdinalIgnoreCase);
            bodyText.text = _currentString;
            onNext.gameObject.SetActive(true);
        }

        public void Close()
        {
            panel.SetActive(false);
        }
    }
}
