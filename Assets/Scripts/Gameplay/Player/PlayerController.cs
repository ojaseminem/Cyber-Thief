using System;
using Gameplay.Managers;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        #region Singleton

        public static PlayerController Instance;
        private void Awake() => Instance = this;

        #endregion

        private CharacterController _controller;
        private Vector3 _move;
        private bool _directionLeft;
        public float speed;
        [SerializeField] private Transform endChecker;
        [SerializeField] private LayerMask endLayerMask;
        private bool _hasReachedEnd;
        
        private bool _isGrounded;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float radius;

        [SerializeField] private float gravity = -9f;
        [SerializeField] private float jumpHeight = 2;
        private Vector3 _velocity;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            _move.x = speed;
            
            _isGrounded = Physics.CheckSphere(groundCheck.position, radius, groundLayer);
            if (_isGrounded && _velocity.y < 0) _velocity.y = -1f;

            _hasReachedEnd = Physics.CheckSphere(endChecker.position, .2f, endLayerMask);
            if(_hasReachedEnd && _isGrounded) ChangeDirection();
            
            if (_isGrounded)
            {
                _controller.center = new Vector3(0, .5f, 0);

                if (SwipeManager.SwipeUp) JumpUp();
                if (SwipeManager.SwipeDown) JumpDown();
            }
            else
            {
                _velocity.y += gravity * Time.deltaTime;
            }
            
            _controller.Move(_velocity * Time.deltaTime);

            if(_directionLeft) _controller.Move(_move * Time.deltaTime);
            if(!_directionLeft) _controller.Move(-_move * Time.deltaTime);
        }

        private void JumpUp()
        {
            //AudioManager.instance.PlaySound("SFX_PlayerJumpUp");
            _controller.center = new Vector3(0, 1.5f, 0);
            _velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
        }
        
        private void JumpDown()
        {
            //AudioManager.instance.PlaySound("SFX_PlayerJumpDown");
            _controller.center = new Vector3(0, 1.5f, 0);
            _velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
        }

        private void ChangeDirection()
        {
            //_hasReachedEnd = false;
            _directionLeft = !_directionLeft;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bitcoin"))
            {
                ScoreManager.Instance.bitcoinCount++;
                ScoreManager.Instance.SetScore();
                AudioManager.Instance.PlaySound("BitcoinCollected");
            }
            if (other.CompareTag("Ethereum"))
            {
                ScoreManager.Instance.ethereumCount += 10;
                ScoreManager.Instance.SetScore();
                AudioManager.Instance.PlaySound("EthereumCollected");
            }
        }
    }
}