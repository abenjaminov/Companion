using _Scripts.States;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class WalkState: State
    {
        private readonly PlayerMovement _playerMovement;
        private readonly Animator _animator;
        
        static readonly int WalkStateHash = Animator.StringToHash("Walk");
        
        public WalkState(PlayerMovement playerMovement, Animator animator)
        {
            _playerMovement = playerMovement;
            _animator = animator;
        }
        
        public override void OnEnter()
        {
            _playerMovement.Walk();
            _animator.CrossFade(WalkStateHash, 0, 0);
        }

        public override void OnExit()
        {
            Debug.Log("Walk State Exit");
        }
    }
}