using System;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

namespace OmbreDiAretua
{
    
public class Player : MonoBehaviour
{
    [SerializeField]  PlayerData _playerData;
    [SerializeField]  List<PlayerMechanics> _playerMechanicsList = new List<PlayerMechanics>();
    [SerializeField]  PlayerUi _playerUi;
    [Header("Animator")] 
    [SerializeField] Animator _animator;
    [SerializeField] string animatorDeath;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        foreach (var playerMechanic in _playerMechanicsList)
        {
            playerMechanic.Init(_playerData);
        }
        _playerUi.Init(_playerData);
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
