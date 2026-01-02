using UnityEngine;

namespace OmbreDiAretua
{
    public abstract class PlayerMechanics : MonoBehaviour
    {
        [SerializeField] protected PlayerData _currentPlayer;

        public virtual void Init(PlayerData playerData)
        {
            _currentPlayer = playerData;
        }

        public virtual void UpdatePlayerData(PlayerData playerData)
        {
            _currentPlayer = playerData;
        }

        public abstract void BlockMechanic();

        public abstract void UnblockMechanic();


        public abstract void HideAllUI();

        public abstract void ShowAllUI();
    }
}