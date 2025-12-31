using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OmbreDiAretua
{


    public class Enemy : MonoBehaviour
    {
        public Action<Enemy> OnDeath;
        public ElementalType ElementalType;
        public int damage;
        public int health;
        public int speedMovement;
        private int remainHealth;
        [SerializeField] private EnemyViewer _enemyViewer;
        [SerializeField] private EnemyBrain _enemyBrain;
        [SerializeField] private Collider2D _collider2D;
        [Header("Animator")] 
        [SerializeField] private Animator _animator;
        [SerializeField] string takeDamage;
        [SerializeField] string death;
        [SerializeField] string movement;
        [SerializeField] string hit;
        
        [SerializeField] bool isInWaitMode;
        private float waitTime;
        
        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _enemyViewer.Setup(health);
            if (_enemyBrain)
            {
                _enemyBrain.Init(this);
            }
            remainHealth = health;
        }

        public void Move()
        {
            _animator.SetBool(movement,true);
            _animator.SetBool(hit,false);
            _animator.SetBool(takeDamage,false);
        }

        public void Attack()
        {
            _animator.SetBool(movement,false);
            _animator.SetBool(hit,true);
            _animator.SetBool(takeDamage,false); 
        }

        public void TakeDamage(int dmg)
        {
            remainHealth  -= dmg;
            _enemyViewer.ShowDamage(dmg);
            _animator.SetBool(movement,false);
            _animator.SetBool(hit,false);
            _animator.SetBool(takeDamage,true);

            if (remainHealth < 0)
            {
                _collider2D.enabled = false;
                _animator.SetBool(death,true);
                OnDeath?.Invoke(this);
                return;
            }
        }

        public void OnAfterTakingDamageAnimation() => _animator.SetBool(takeDamage, false);
        
        public void OnAfterDeathAnimation() => Destroy(gameObject);

        public bool IsInWaitMode()
        {
            return isInWaitMode;
        }

        public void SetWaitMode(float timerWait)
        {
            waitTime = timerWait;
            isInWaitMode = true;
            StartCoroutine(Wait());
            
            IEnumerator Wait()
            {
                yield return new WaitForSeconds(waitTime);
                isInWaitMode = false;

            }

        }


    }
    
}
