using _Scripts.Player.States;
using _Scripts.ScriptableObjects.Channels;
using _Scripts.States;
using _Scripts.Systems.InputSystem;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerGameState))]
    [RequireComponent(typeof(PlayerAnimations))]
    public class PlayerStateMachine : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private PlayerChannel playerChannel;
        [SerializeField] private Animator _animator;
        private PlayerMovement _playerMovement;
        private PlayerGameState _playerGameState;
        private PlayerAnimations _playerAnimations;
        
        private StateMachine _stateMachine;

        private JumpState _jumpState;
        private WalkState _walkState;
        private RunState _runState;
        private IdleState _idleState;
        
        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerGameState = GetComponent<PlayerGameState>();
            _playerAnimations = GetComponent<PlayerAnimations>();

            SetupStateMachine();
            
            inputReader.OnToggleWeaponClickedEvent += OnToggleWeaponClickedEvent;
        }

        private void OnToggleWeaponClickedEvent()
        {
            
        }

        private void SetupStateMachine()
        {
            _stateMachine = new StateMachine("Player");
            
            _idleState = new IdleState(_playerMovement, _animator, _playerAnimations);
            _walkState = new WalkState(_playerMovement, _animator, _playerAnimations);
            _jumpState = new JumpState(playerChannel, _playerMovement, _animator);
            _runState = new RunState(_playerMovement, _animator, _playerAnimations);
            
            _stateMachine.AddTransition(_idleState, _walkState, () => inputReader.IsMoving && !inputReader.IsRunning);
            _stateMachine.AddTransition(_idleState, _jumpState, () => inputReader.IsJump);
            _stateMachine.AddTransition(_idleState, _runState, () => inputReader.IsMoving && inputReader.IsRunning);
            
            _stateMachine.AddTransition(_walkState, _idleState, () => !inputReader.IsMoving);
            _stateMachine.AddTransition(_walkState, _jumpState, () => inputReader.IsJump);
            _stateMachine.AddTransition(_walkState, _runState, () => inputReader.IsMoving && inputReader.IsRunning);
            
            _stateMachine.AddTransition(_jumpState, _walkState, () => _jumpState.IsJumpOver && !inputReader.IsJump && inputReader.IsMoving && !inputReader.IsRunning);
            _stateMachine.AddTransition(_jumpState, _idleState, () => _jumpState.IsJumpOver && !inputReader.IsJump && !inputReader.IsMoving);
            _stateMachine.AddTransition(_jumpState, _runState, () => _jumpState.IsJumpOver && !inputReader.IsJump && inputReader.IsMoving && !inputReader.IsRunning);
            
            _stateMachine.AddTransition(_runState, _walkState, () => inputReader.IsMoving && !inputReader.IsRunning);
            _stateMachine.AddTransition(_runState, _jumpState, () => inputReader.IsJump);
            _stateMachine.AddTransition(_runState, _idleState, () => !inputReader.IsMoving);
            
            _stateMachine.SetState(_idleState);
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}