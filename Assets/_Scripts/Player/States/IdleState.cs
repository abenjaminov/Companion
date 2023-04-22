using _Scripts.States;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class IdleState : State
    {
        private readonly Animator _animator;
        private PlayerMovement _playerMovement;
        
        static readonly int IdleStateHash = Animator.StringToHash("Idle");
        
        public IdleState(PlayerMovement playerMovement, Animator animator)
        {
            _animator = animator;
            _playerMovement = playerMovement;
            
        }
        
        public override void OnEnter()
        {
            _playerMovement.Idle();
            _animator.CrossFade(IdleStateHash, 0,0);
        }

        public override void OnExit()
        {
        }
    }
}