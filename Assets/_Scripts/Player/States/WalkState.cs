using _Scripts.Player.Types;
using _Scripts.States;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class WalkState: State
    {
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerGameState _playerGameState;
        private readonly Animator _animator;

        private int _currentStateHash = -1;
        
        public WalkState(PlayerMovement playerMovement, Animator animator, PlayerGameState playerGameState)
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
                newStateHash = animationHashDict[StateType.Walk];
                
                if (_playerMovement.Velocity.x < 0)
                {
                    newStateHash = animationHashDict[StateType.WalkStrafeLeft];;
                }
                else if(_playerMovement.Velocity.x > 0)
                {
                    newStateHash = animationHashDict[StateType.WalkStrafeRight];;
                }
            }
            else if (_playerMovement.Velocity.z < 0)
            {
                newStateHash = animationHashDict[StateType.WalkBack];;
                
                if (_playerMovement.Velocity.x < 0)
                {
                    newStateHash = animationHashDict[StateType.WalkStrafeLeftBack];;
                }
                else if(_playerMovement.Velocity.x > 0)
                {
                    newStateHash = animationHashDict[StateType.WalkStrafeRightBack];;
                }
            }

            if (newStateHash == _currentStateHash) return;
            
            _animator.CrossFade(newStateHash,.25f,0);

            _currentStateHash = newStateHash;
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