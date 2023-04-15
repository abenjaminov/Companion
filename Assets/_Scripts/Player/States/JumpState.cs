using _Scripts.ScriptableObjects.Channels;
using _Scripts.States;
using UnityEngine;

namespace _Scripts.Player.States
{
    public class JumpState: State
    {
        public bool IsJumpOver;
        private readonly PlayerChannel _playerChannel;
        private readonly PlayerMovement _playerMovement;
        private readonly Animator _animator;

        static readonly int JumpStateHash = Animator.StringToHash("Jump");
        
        public JumpState(PlayerChannel playerChannel, PlayerMovement playerMovement, Animator animator)
        {
            _playerChannel = playerChannel;
            _playerMovement = playerMovement;
            _animator = animator;
        }
        
        public override void OnEnter()
        {
            IsJumpOver = false;
            _playerChannel.OnPlayerJumpAnimationEndEvent += OnPlayerJumpAnimationEndEvent;
            _animator.CrossFade(JumpStateHash, 0, 0);
        }

        private void OnPlayerJumpAnimationEndEvent()
        {
            IsJumpOver = true;
        }

        public override void OnExit()
        {
            _playerChannel.OnPlayerJumpAnimationEndEvent -= OnPlayerJumpAnimationEndEvent;
        }
    }
}