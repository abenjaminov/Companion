using _Scripts.Systems.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace _Scripts.Systems.InputSystem
{
    public class InputReader : MonoBehaviour, InputActions.IPlayerGameplayActions
    {
        [HideInInspector] public Vector2 movementDirection;
        [HideInInspector] public bool IsRunning;
        [HideInInspector] public bool IsJump;
        [HideInInspector] public Vector2 MouseDelta;
        [HideInInspector] public bool IsMoving;
        [HideInInspector] public bool IsAiming;
        [HideInInspector] public bool IsShooting;

        public UnityAction OnToggleWeaponClickedEvent;
        public UnityAction OnShootStartEvent;
        public UnityAction OnShootEndEvent;

        private InputActions _controls;
        
        private void OnEnable()
        {
            if (_controls != null)
                return;

            _controls = new InputActions();
            _controls.PlayerGameplay.SetCallbacks(this);
            _controls.PlayerGameplay.Enable();
        }

        public void OnDisable()
        {
            _controls.PlayerGameplay.Disable();
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            movementDirection = context.ReadValue<Vector2>();

            IsMoving = movementDirection != Vector2.zero;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            IsJump = context.performed;
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            MouseDelta = context.ReadValue<Vector2>();
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                IsRunning = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                IsRunning = false;
            }
        }

        public void OnToggleWeapon(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            
            OnToggleWeaponClickedEvent?.Invoke();
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                IsAiming = true;
            }
            else if (context.canceled)
            {
                IsAiming = false;
            }
        }
        
        public void OnShoot(InputAction.CallbackContext context)
        {
            Debug.Log("Shoot Input");
            if (context.performed)
            {
                IsShooting = true;
                OnShootStartEvent?.Invoke();
            }
            else if (context.canceled)
            {
                IsShooting = false;
                OnShootEndEvent?.Invoke();
            }
        }
    }
}