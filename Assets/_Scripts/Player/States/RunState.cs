using _Scripts.States;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class RunState : State
    {
        private readonly PlayerMovement _playerMovement;
        private readonly Animator _animator;
        
        static readonly int RunStateHash = Animator.StringToHash("Run");
        static readonly int RunStafeLeftStateHash = Animator.StringToHash("Run Strafe Left");
        static readonly int RunStrafeRightStateHash = Animator.StringToHash("Run Strafe Right");
        
        private int currentStateHash = -1;
        
        public RunState(PlayerMovement playerMovement, Animator animator)
        {
            _playerMovement = playerMovement;
            _animator = animator;
        }
        
        private void SetAnimation()
        {
            int newStateHash = RunStateHash;

            if (_playerMovement.Velocity.z > 0)
            {
                if (_playerMovement.Velocity.x < 0)
                {
                    newStateHash = RunStafeLeftStateHash;
                }
                else if(_playerMovement.Velocity.x > 0)
                {
                    newStateHash = RunStrafeRightStateHash;
                }
            }

            if (newStateHash == currentStateHash) return;
            
            _animator.CrossFade(newStateHash,.25f,0);

            currentStateHash = newStateHash;
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
            Debug.Log("Run State Exit");
        }
    }
}