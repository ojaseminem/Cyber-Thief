using System;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Enemy
{
    [RequireComponent(typeof(CharacterController))]
    public class EnemyAI : MonoBehaviour
    {
        private CharacterController _controller;
        private Vector3 _move;
        [SerializeField] private bool _directionLeft;
        public bool canMove;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float attackSpeed;
        private bool _attacking;
        [SerializeField] private Transform endChecker;
        [SerializeField] private LayerMask endLayerMask;
        private bool _hasReachedEnd;
        private bool _isDead;

        [SerializeField] private Animator anim;
        private static readonly int Blend = Animator.StringToHash("Blend");

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            canMove = true;
            ChangeDirection();
        }

        private void OnEnable()
        {
            anim.SetFloat(Blend, _directionLeft ? 1 : 2);
        }

        private void Update()
        {
            if(!_isDead)
            {
                if (canMove)
                {
                    _move.x = _attacking ? attackSpeed : movementSpeed;

                    _hasReachedEnd = Physics.CheckSphere(endChecker.position, .3f, endLayerMask);
                    if (_hasReachedEnd) ChangeDirection();


                    if (!_directionLeft)
                    {
                        _controller.Move(-_move * Time.deltaTime);
                    }
                    else
                    {
                        _controller.Move(_move * Time.deltaTime);
                    }

                    //Attacking
                    var selfPos = transform.position;
                    if (Physics.Raycast(new Vector3(selfPos.x, selfPos.y + .5f, selfPos.z),
                            _directionLeft ? transform.right : -transform.right,
                            out var hit, 7))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            Attacking();
                        }
                    }
                    else
                    {
                        _attacking = false;
                        anim.SetFloat(Blend, _directionLeft ? 1 : 2);
                    }
                }
            }
        }

        private void Attacking()
        {
            _attacking = true;
            anim.SetFloat(Blend, _directionLeft ? 3 : 4);
        }

        private void ChangeDirection()
        {
            _attacking = false;
            
            _directionLeft = !_directionLeft;
            
            anim.SetFloat(Blend, _directionLeft ? 1 : 2);
        }

        private void Death()
        {
            _isDead = true;
            anim.SetFloat(Blend, 0);
            Destroy(gameObject, 1f);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.collider.CompareTag("Player"))
            {
                if(_attacking)
                {
                    PlayerController.Instance.Death();
                    Death();
                }
            }
        }
    }
}