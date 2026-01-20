using System;
using System.Collections;
using UnityEngine;

namespace OmbreDiAretua
{
    public class PlayerMovement : PlayerMechanics
    {
        [SerializeField] Transform _PlayerSpriteTransform;
        [SerializeField] float clampVelocity;
        [SerializeField] bool canMove;
        [SerializeField] private LayerMask blockerMask;
        [Header("Animator")] 
        [SerializeField] Animator _animator;
        [SerializeField] string moving;
        [Header("Music")] 
        [SerializeField] private SfxPlayer _soundWalking;

        [Header("FeedbackSlow")] [SerializeField]
        private GameObject _feedbackSlow;
        float speedMutliplier = 1;
         public override void Init(PlayerData playerData)
        {
            base.Init(playerData);
            canMove = true;
        }

        public override void BlockMechanic()
        {
            canMove = false;
        }

        public override void UnblockMechanic()
        {
            canMove = true;
        }

        public override void HideAllUI()
        {
            
        }

        public override void ShowAllUI()
        {
        }


        private void Update()
        {
            if (!canMove)
            {
                return;
            }
            
            var horizontal = Input.GetAxis("Horizontal") * _currentPlayer.speed* speedMutliplier * Time.deltaTime;
            var vertical = Input.GetAxis("Vertical") * _currentPlayer.speed * speedMutliplier * Time.deltaTime;
            var ismoving = horizontal != 0 || vertical != 0;
            _animator.SetBool(moving,ismoving);
            if (horizontal != 0)
            {
                var sizeX = horizontal < 0 ? -1 : 1;
                var value = Mathf.Abs( _PlayerSpriteTransform.localScale.x ) * Mathf.Sign(sizeX);
                _PlayerSpriteTransform.localScale = new Vector3(value, _PlayerSpriteTransform.localScale.y);
            }
            transform.position += new Vector3(horizontal, vertical); }

        private bool isSlowed = false;
        
        public void DebuffSlowPlayer(float timer,float speedMid)
        {
            if (isSlowed)
            {
                return;   
            }

            isSlowed = true;
            _feedbackSlow.SetActive(true);
            speedMutliplier = speedMid;
            StartCoroutine(DebuffSlowPlayerCor(timer));
        }

        IEnumerator DebuffSlowPlayerCor(float timer)
        {
            yield return new WaitForSeconds(timer);
            isSlowed = false;
            speedMutliplier = 1;
            _feedbackSlow.SetActive(false);
        }
    }
}