using System;
using System.Collections;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OmbreDiAretua
{
    public class PowerUpChoicer : MonoBehaviour
    {
        public Action<SpellData,PowerUpChoicer> OnClicked;
        public UnityEvent OnSelected;
        private SpellData _spellData;
        [SerializeField] private Button _button;
        [SerializeField] RectTransform choicerTransform;
        [Header("Textes")] 
        [SerializeField] private SerializedDictionary<int, string> descriptionBasedOnlevel = new();
        [SerializeField] TextMeshProUGUI _levelText;
        [SerializeField] TextMeshProUGUI _nameSpell;
        [SerializeField] TextMeshProUGUI _descriptionSpell;
        [SerializeField] Image _spellImage;
        [SerializeField] private EventTrigger _eventTrigger;
        [Header("TriggerAnimation")]
        [SerializeField] float timerEnterExit;
        [SerializeField] float timerChangeSize;
        [SerializeField] private float maxSize;

        private void Start()
        {
            _button.onClick.AddListener(Select);
        }

        public void Init(SpellData spellData,int level)
        {
            _spellData = spellData;
            _eventTrigger.enabled = false;
            StopAllCoroutines();
            StartCoroutine(EnterIn());
            _levelText.text = $"Lv.{level+1}";
            _spellImage.sprite = spellData.spriteSpell;
            _descriptionSpell.text = descriptionBasedOnlevel[level];
            _nameSpell.text = spellData.nameSpell;

        }

        public void DeInit()
        {
            _button.interactable = false;
            _eventTrigger.enabled = false;
            StopAllCoroutines();
            StartCoroutine(ExitOut());
        }


        public void SizeIn()
        {
            StopAllCoroutines();
            StartCoroutine(ChangeSize(maxSize));

        }

    
        public void SizeOut()
        {
            StopAllCoroutines();
            StartCoroutine(ChangeSize(1));
        }

        public void Select()
        {
            Debug.Log("Clicked");
            _button.interactable = false;
            OnSelected?.Invoke();
            OnClicked?.Invoke(_spellData,this);
        }

        private IEnumerator ChangeSize(float sizeEnd)
        {
            float t = 0f;
            float currentSize = choicerTransform.localScale.x;
        
            while (t < timerChangeSize)
            {
                choicerTransform.localScale = Vector3.Lerp(Vector3.one * currentSize,Vector3.one * sizeEnd,t/timerChangeSize);        
                t += Time.deltaTime;
                yield return null;
            }

            choicerTransform.localScale = Vector3.one * sizeEnd;
        }
    
        private IEnumerator EnterIn()
        {
            _button.interactable = false;
            float t = 0f;
            choicerTransform.localScale = Vector3.zero;
            float timer = timerEnterExit / 2;
            while (t < timer)
            {
                choicerTransform.localScale = Vector3.Lerp(Vector3.zero,Vector3.one*maxSize,t/timer);
                t += Time.deltaTime;
                yield return null;
            }
        
            choicerTransform.localScale = Vector3.one * maxSize;
            t = 0f;
            while (t < timer)
            {
                choicerTransform.localScale = Vector3.Lerp(Vector3.one * maxSize,Vector3.one, t/timer);
                t += Time.deltaTime;
                yield return null;
            }

            _button.interactable = true;
            _eventTrigger.enabled = true;
        }
    
        private IEnumerator ExitOut()
        {
            _button.interactable = false;
            float t = 0f;
            choicerTransform.localScale = Vector3.one;
            float timer = timerEnterExit / 2;
            while (t < timer)
            {
                choicerTransform.localScale = Vector3.Lerp(Vector3.one,Vector3.one*maxSize,t/timer);
                t += Time.deltaTime;
                yield return null;
            }
        
            choicerTransform.localScale = Vector3.one * maxSize;
            t = 0f;
            while (t < timer)
            {
                choicerTransform.localScale = Vector3.Lerp(Vector3.one * maxSize,Vector3.zero, t/timer);
                t += Time.deltaTime;
                yield return null;
            }
        
        }
    
    }
}