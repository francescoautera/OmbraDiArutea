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
        public GameObject remainEnemiesObject;
        public TextMeshProUGUI remainEnemy;
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

        public void ShowRemainEnemies(int remainEnemies)
        {
            remainEnemiesObject.SetActive(true);
            remainEnemy.text = $"Nemici Rimasti: {remainEnemies}";
        }

        public void CloseShowRemainEnemies() => remainEnemiesObject.SetActive(false);
    }
}