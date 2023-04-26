using _Scripts.Player.Types;
using _Scripts.States;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class RunState : State
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerAnimations _playerAnimations;
        private readonly Animator _animator;

        private int _currentStateHash = -1;
        
        public RunState(PlayerMovement playerMovement, Animator animator, PlayerAnimations playerAnimations)
        {
            _playerMovement = playerMovement;
            _animator = animator;
            _playerAnimations = playerAnimations;
        }
        
        private void SetAnimation()
        {
            var stateType = StateType.Run;
            
            if (_playerMovement.LocalMovementDirection.z > 0)
            {
                if (_playerMovement.LocalMovementDirection.x < 0)
                {
                    stateType = StateType.RunStrafeLeft;
                }
                else if(_playerMovement.LocalMovementDirection.x > 0)
                {
                    stateType = StateType.RunStrafeRight;
                }
            }
            else if (_playerMovement.LocalMovementDirection.z < 0)
            {
                stateType = StateType.RunBack;
                
                if (_playerMovement.LocalMovementDirection.x < 0)
                {
                    stateType = StateType.RunStrafeLeftBack;
                }
                else if(_playerMovement.LocalMovementDirection.x > 0)
                {
                    stateType = StateType.RunStrafeRightBack;
                }
            }

            _playerAnimations.UpdateAnimations(stateType);
        }
        
        public override void OnEnter()
        {
            _playerMovement.Run();
            SetAnimation();
        }

        public override void Tick()
        {
            _playerMovement.Run();
            SetAnimation();
        }

        public override void OnExit()
        {
            _currentStateHash = -1;
        }
    }
}