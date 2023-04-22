using _Scripts.Player.Types;
using _Scripts.States;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class IdleState : State
    {
        private readonly Animator _animator;
        private PlayerMovement _playerMovement;
        private PlayerAnimations _playerAnimations;
        
        static readonly int IdleStateHash = Animator.StringToHash("Idle");
        
        public IdleState(PlayerMovement playerMovement, Animator animator, PlayerAnimations playerAnimations)
        {
            _animator = animator;
            _playerMovement = playerMovement;
            _playerAnimations = playerAnimations;
        }
        
        public override void OnEnter()
        {
            _playerMovement.Idle();
        }

        public override void Tick()
        {
            _playerAnimations.UpdateAnimations(StateType.Idle);
        }
    }
}