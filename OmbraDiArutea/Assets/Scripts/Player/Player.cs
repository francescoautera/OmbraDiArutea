using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

namespace OmbreDiAretua
{

    public class Player : MonoBehaviour
    {
        [SerializeField] PlayerData _playerData;
        [SerializeField] List<PlayerMechanics> _playerMechanicsList = new List<PlayerMechanics>();
        [SerializeField] PlayerUi _playerUi;
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private float timerInvibility;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private bool isInvincibily = false;
        [SerializeField] private GameObject _invincibilityShield;
        [SerializeField] private bool initAtStart;
        [Header("Animator")] [SerializeField] Animator _animator;
        [SerializeField] string animatorDeath;

        public bool IsInvincibily => isInvincibily;

        private void Start()
        {

            if (initAtStart)
            {
                Init();
            }
        }

        public void Init()
        {
            foreach (var playerMechanic in _playerMechanicsList)
            {
                playerMechanic.Init(_playerData);
            }

            _playerUi.Init(_playerData);
        }

        public void StopAll()
        {
            foreach (var playerMechanic in _playerMechanicsList)
            {
                playerMechanic.BlockMechanic();
            }
        }

        public void HideAllUI()
        {
            foreach (var playerMechanic in _playerMechanicsList)
            {
                playerMechanic.HideAllUI();
            }
            _playerUi.HideAll();
        }

        public void ShowAllUI()
        {
            foreach (var playerMechanic in _playerMechanicsList)
            {
                playerMechanic.ShowAllUI();
            }
            
            _playerUi.ShowAll();
        }

        public void RestartAll()
    {
        foreach (var playerMechanic in _playerMechanicsList)
        {
            playerMechanic.UnblockMechanic();
        }
    }

    [Button]
    private void ChangeHealth(int health)
    {
        _playerData.health = health;
        _playerUi.UpdateHealth(_playerData);
        if (health <= 0)
        {
            _animator.SetBool(animatorDeath,true);
            foreach (var playerMechanics in _playerMechanicsList)
            {
                playerMechanics.BlockMechanic();
            }
        }
    }

    public void AddHealth(int health)
    {
        _playerData.health += health;
        _playerUi.UpdateHealth(_playerData);
        if (_playerData.health <= 0)
        {
            StopAllCoroutines();
            GetComponentInChildren<Collider2D>().enabled = false;
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _invincibilityShield.SetActive(false);
            _animator.SetBool(animatorDeath,true);
            foreach (var playerMechanics in _playerMechanicsList)
            {
                playerMechanics.BlockMechanic();
            }
            GameGlobalEvents.OnPlayerDeath?.Invoke();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hitted : " + other.name);
        
            
        if (isInvincibily)
        {
            return;
        }
        
        if ((enemyMask.value & (1 << other.gameObject.layer)) != 0)
        {
            if (_playerData.health <= 0)
            {
                return;
            }
            var enemy = other.GetComponent<Enemy>();
            var damage = Mathf.Clamp(enemy.damage - _playerData.defence,0,_playerData.health);
            AddHealth(-damage);
            StartCoroutine(InvincibilyCor());
        }
    }
    private IEnumerator InvincibilyCor()
    {
        float t = 0f;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _invincibilityShield.SetActive(true);
        isInvincibily = true;
        while (t < timerInvibility)
        {
            if (_playerData.health <=0)
            {
                _invincibilityShield.SetActive(false);
            }
            t += Time.deltaTime;
            yield return null;
        }
        _invincibilityShield.SetActive(false);
        isInvincibily = false;
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
}

[Serializable]
public class PlayerData
{
    public int force;
    public int defence;
    public float speed;
    public int health;
}
}
