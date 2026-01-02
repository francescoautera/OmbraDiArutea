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
        [Header("BurnSection")]
        [SerializeField] bool isBurned;
        private float timerToBurn;
        private float currentTimerBurned;
        private int fireDamage;
        [Header("LowerSection")] 
        [SerializeField] bool isSlowing;
        private float timerSlow;
        private float currentTimerSlow;
        
        
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

        public void SetFire(float timerBurned, int fireDamage)
        {
            if (isBurned || isSlowing)
            {
                return;
            }
            _enemyViewer.UpdateFire(true);
            isBurned = true;
            currentTimerBurned = 0f;
            timerToBurn = timerBurned;
            this.fireDamage = fireDamage;
            StartCoroutine(FireDamageCor());
        }

        public void SetSlow(float timerSlow)
        {
            if (isBurned || isSlowing)
            {
                return;
            }
            _enemyViewer.UpdateSlow(true);
            speedMovement /= 2;
            isSlowing = true;
            currentTimerSlow = 0f;
            this.timerSlow = timerSlow;
            StartCoroutine(SlowCor());
        }
        
        IEnumerator SlowCor()
        {
            while (currentTimerSlow < timerSlow)
            {
                yield return new WaitForSeconds(1f);
                currentTimerSlow += 1;
            }

            isSlowing = false;
            speedMovement *= 2;
            _enemyViewer.UpdateSlow(false);

        }


        IEnumerator FireDamageCor()
        {
            while (currentTimerBurned < timerToBurn)
            {
                yield return new WaitForSeconds(1f);
                currentTimerBurned += 1;
                TakeDamage(fireDamage);
            }

            isBurned = false;
            _enemyViewer.UpdateFire(false);

        }

    }
    
}
