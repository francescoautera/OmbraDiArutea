using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{


    public class Enemy : MonoBehaviour
    {
        public ElementalType ElementalType;
        public int damage;
        public int health;
        public int speedMovement;
        private int remainHealth;
        [SerializeField] private EnemyViewer _enemyViewer;
        [SerializeField] private Collider2D _collider2D;
        [Header("Animator")] 
        [SerializeField] private Animator _animator;
        [SerializeField] string takeDamage;
        [SerializeField] string death;
        [SerializeField] string movement;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _enemyViewer.Setup(health);
            remainHealth = health;
        }

        public void TakeDamage(int dmg)
        {
            remainHealth  -= dmg;
            _enemyViewer.ShowDamage(dmg);
            if (remainHealth < 0)
            {
                _collider2D.enabled = false;
                Destroy(gameObject);
                return;
            }
        }
    }
}
