using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OmbreDiAretua
{
    public class ButtonMultiChoice : MonoBehaviour
    {
        public Action onClick;
        [SerializeField] TextMeshProUGUI body;
        [SerializeField] private Button _click;

        private void Start()
        {
            _click.onClick.AddListener(() => { onClick?.Invoke();});
        }

        public void Setup(string text)
        {
            body.text = text;
        }
    }
}