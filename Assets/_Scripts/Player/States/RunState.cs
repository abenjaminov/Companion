using _Scripts.Player.Types;
using _Scripts.States;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class RunState : State
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerGameState _playerGameState;
        private readonly Animator _animator;

        private int _currentStateHash = -1;
        
        public RunState(PlayerMovement playerMovement, Animator animator, PlayerGameState playerGameState)
        {
            _playerMovement = playerMovement;
            _animator = animator;
            _playerGameState = playerGameState;
        }
        
        private void SetAnimation()
        {
            var animationHashDict =
                PlayerConsts.WeaponTypeToStateTypeToAnimationHash[_playerGameState.CurrentWeaponType];
            
            int newStateHash = _currentStateHash;

            if (_playerMovement.Velocity.z > 0)
            {
                newStateHash = animationHashDict[StateType.Run];
                if (_playerMovement.Velocity.x < 0)
                {
                    newStateHash = animationHashDict[StateType.RunStrafeLeft];
                }
                else if(_playerMovement.Velocity.x > 0)
                {
                    newStateHash = animationHashDict[StateType.RunStrafeRight];
                }
            }
            else if (_playerMovement.Velocity.z < 0)
            {
                newStateHash = animationHashDict[StateType.RunBack];
                
                if (_playerMovement.Velocity.x < 0)
                {
                    newStateHash = animationHashDict[StateType.RunStrafeLeftBack];
                }
                else if(_playerMovement.Velocity.x > 0)
                {
                    newStateHash = animationHashDict[StateType.RunStrafeRightBack];
                }
            }

            if (newStateHash == _currentStateHash) return;
            
            _animator.CrossFade(newStateHash,.25f,0);

            _currentStateHash = newStateHash;
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