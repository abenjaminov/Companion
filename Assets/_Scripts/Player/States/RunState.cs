using _Scripts.States;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class RunState : State
    {
        private readonly PlayerMovement _playerMovement;
        private readonly Animator _animator;
        
        static readonly int RunStateHash = Animator.StringToHash("Run");
        
        public RunState(PlayerMovement playerMovement, Animator animator)
        {
            _playerMovement = playerMovement;
            _animator = animator;
        }
        
        public override void OnEnter()
        {
            _playerMovement.Run();
            _animator.CrossFade(RunStateHash, 0, 0);
        }

        public override void OnExit()
        {
            Debug.Log("Run State Exit");
        }
    }
}