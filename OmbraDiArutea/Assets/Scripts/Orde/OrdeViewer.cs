using System;
using TMPro;
using UnityEngine;

namespace OmbreDiAretua
{
    public class OrdeViewer : MonoBehaviour
    {
        public Animator _ordeAnimatorView;
        public string show;
        public TextMeshProUGUI ordeNumber;
        private Action OnEndShow;

        public void ShowNextOrde(int ordeNumber,Action OnEnd)
        {
            OnEndShow = OnEnd;
            this.ordeNumber.text = $"Orda numero {ordeNumber}";
            _ordeAnimatorView.SetBool(show,true);
        }

        public void OnEndShowOrde()
        {
            _ordeAnimatorView.SetBool(show,false);
            OnEndShow?.Invoke();
        }
    }
}