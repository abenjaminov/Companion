using _Scripts.States;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class IdleState : State
    {
        private readonly Animator _animator;
        private PlayerMovement _playerMovement;
        
        public IdleState(PlayerMovement playerMovement, Animator animator)
        {
            _animator = animator;
            _playerMovement = playerMovement;
        }
        
        public override void OnEnter()
        {
            _playerMovement.Idle();
            _animator.CrossFade("Idle", 0,0);
        }

        public override void OnExit()
        {
            Debug.Log("Idle State Exit");
        }
    }
}