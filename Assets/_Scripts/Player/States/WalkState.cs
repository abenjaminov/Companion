using _Scripts.Player.Types;
using _Scripts.States;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class WalkState: State
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerAnimations _playerAnimations;
        private readonly Animator _animator;

        private int _currentStateHash = -1;
        
        public WalkState(PlayerMovement playerMovement, Animator animator, PlayerAnimations playerAnimations)
        {
            _playerMovement = playerMovement;
            _animator = animator;
            _playerAnimations = playerAnimations;
        }

        private void SetAnimation()
        {
            var stateType = StateType.Walk;

            if (_playerMovement.LocalMovementDirection.z > 0)
            {
                stateType = StateType.Walk;
                
                if (_playerMovement.LocalMovementDirection.x < 0)
                {
                    stateType = StateType.WalkStrafeLeft;
                }
                else if(_playerMovement.LocalMovementDirection.x > 0)
                {
                    stateType = StateType.WalkStrafeRight;
                }
            }
            else if (_playerMovement.LocalMovementDirection.z < 0)
            {
                stateType = StateType.WalkBack;
                
                if (_playerMovement.LocalMovementDirection.x < 0)
                {
                    stateType = StateType.WalkStrafeLeftBack;
                }
                else if(_playerMovement.LocalMovementDirection.x > 0)
                {
                    stateType = StateType.WalkStrafeRightBack;
                }
            }

            _playerAnimations.UpdateAnimations(stateType);
        }
        
        public override void OnEnter()
        {
            _playerMovement.Walk();
            SetAnimation();
        }

        public override void Tick()
        {
            _playerMovement.Walk();
            SetAnimation();
        }

        public override void OnExit()
        {
            _currentStateHash = -1;
        }
    }
}