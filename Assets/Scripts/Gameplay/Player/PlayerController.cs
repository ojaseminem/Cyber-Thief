using System;
using System.Collections;
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

        //Movement Variables
        private CharacterController _controller;
        private Vector3 _move;
        private bool _directionLeft;
        public float speed;
        [SerializeField] private Transform endChecker;
        [SerializeField] private LayerMask endLayerMask;
        private bool _hasReachedEnd;
        [SerializeField] private Transform skmCharacter;
        private bool _canChangeDirection;
        
        //Jump Variables
        private bool _isGrounded;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float radius;
        [SerializeField] private float gravity = -9f;
        [SerializeField] private float jumpHeight = 2;
        private Vector3 _velocity;
        
        //Attack Variables
        [HideInInspector] 
        public bool canAttackGlobal;
        private bool _canAttackPrivate;

        //Death Variables
        private bool _isDead;
        [SerializeField] private Transform characterDeathPose;
        
        //Animation Variables
        [SerializeField] private Animator anim;
        private static readonly int Blend = Animator.StringToHash("BlendTreeValue");
        private static readonly int Replay = Animator.StringToHash("Replay");
        private static readonly int Death1 = Animator.StringToHash("Death");

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _canChangeDirection = true;
        }

        private void Update()
        {
            if(!_isDead)
            {
                skmCharacter.localPosition = new Vector3(0, 0, 0);
                _move.x = speed;

                _isGrounded = Physics.CheckSphere(groundCheck.position, radius, groundLayer);
                if (_isGrounded && _velocity.y < 0) _velocity.y = -1f;

                _hasReachedEnd = Physics.CheckSphere(endChecker.position, .2f, endLayerMask);
                if (_hasReachedEnd && _isGrounded && _canChangeDirection) ChangeDirection();

                if (_isGrounded)
                {
                    _controller.center = new Vector3(0, .5f, 0);

                    if (SwipeManager.SwipeUp) JumpUp();
                    if (SwipeManager.SwipeDown) JumpDown();
                    /*_canAttackPrivate = true;
                    if (SwipeManager.SwipeLeft && _canAttackPrivate && canAttackGlobal) Attack(0);
                    if (SwipeManager.SwipeRight && _canAttackPrivate && canAttackGlobal) Attack(1);*/
                }
                else
                {
                    _velocity.y += gravity * Time.deltaTime;
                    //_canAttackPrivate = false;
                }

                _controller.Move(_velocity * Time.deltaTime);

                if (_directionLeft) _controller.Move(_move * Time.deltaTime);
                if (!_directionLeft) _controller.Move(-_move * Time.deltaTime);
            }
        }

        private void JumpUp()
        {
            //AudioManager.instance.PlaySound("SFX_PlayerJumpUp");
            _controller.center = new Vector3(0, 1.5f, 0);
            _velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
            anim.SetTrigger(Replay);
            anim.SetFloat(Blend, 3);
            Invoke(nameof(RunAnimDirectionSetter), 1f);
        }
        
        private void JumpDown()
        {
            //AudioManager.instance.PlaySound("SFX_PlayerJumpDown");
            _controller.center = new Vector3(0, 1.5f, 0);
            _velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
            anim.SetFloat(Blend, 4);
            anim.SetTrigger(Replay);
        }

        private void ChangeDirection()
        {
            _canChangeDirection = false;
            _directionLeft = !_directionLeft;
            RunAnimDirectionSetter();
            Invoke(nameof(ResetCanChangeDirection), 1f);
        }

        private void ResetCanChangeDirection()
        {
            _canChangeDirection = true;
        }
        
        private void RunAnimDirectionSetter()
        {
            anim.SetFloat(Blend, _directionLeft ? 1 : 2);
        }

        /*private void Attack(int dir)
        {
            
        }

        private IEnumerator AttackCoroutineLeft()
        {
            yield return new WaitForSeconds(1f);
        }*/

        public void Death()
        {
            StartCoroutine(DeathAnim());

            IEnumerator DeathAnim()
            {
                _isDead = true;
                GameManager.PlayerDead = true;
                speed = 0;
                _controller.enabled = false;
                anim.ResetTrigger(Replay);
                anim.SetTrigger(Death1);
                yield return new WaitForSeconds(1.2f);
                skmCharacter.gameObject.SetActive(false);
                characterDeathPose.gameObject.SetActive(true);
                yield return new WaitForSeconds(2f);
                GameManager.Instance.GameOver();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("LanOutput"))
            {
                PlayerManager.Instance.PlayerCompletedLevel();
                LevelManager.Instance.SwitchLevelScenes(1);
            }
            if (other.CompareTag("Death"))
            {
                Death();
            }
        }
    }
}