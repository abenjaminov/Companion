using _Scripts.States;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class WalkState: State
    {
        private readonly PlayerMovement _playerMovement;
        private readonly Animator _animator;
        
        static readonly int WalkStateHash = Animator.StringToHash("Walk");
        static readonly int WalkStrafeLeftStateHash = Animator.StringToHash("Walk Strafe Left");
        static readonly int WalkStrafeRightStateHash = Animator.StringToHash("Walk Strafe Right");

        private int currentStateHash = -1;
        
        public WalkState(PlayerMovement playerMovement, Animator animator)
        {
            _playerMovement = playerMovement;
            _animator = animator;
        }

        private void SetAnimation()
        {
            int newStateHash = WalkStateHash;

            if (_playerMovement.Velocity.z > 0)
            {
                if (_playerMovement.Velocity.x < 0)
                {
                    newStateHash = WalkStrafeLeftStateHash;
                }
                else if(_playerMovement.Velocity.x > 0)
                {
                    newStateHash = WalkStrafeRightStateHash;
                }
            }

            if (newStateHash == currentStateHash) return;
            
            _animator.CrossFade(newStateHash,.25f,0);

            currentStateHash = newStateHash;
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
            currentStateHash = -1;
        }
    }
}