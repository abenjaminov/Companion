using System;
using _Scripts.Systems.InputSystem;
using UnityEngine;

namespace _Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;

        [SerializeField] private InputReader inputReader;
        
        [HideInInspector] public Vector3 Velocity;
        [HideInInspector] public Vector3 LocalMovementDirection;
        private CharacterController _characterController;
        
        private void Start()
        {
            Velocity.y = Physics.gravity.y;
            _characterController = GetComponent<CharacterController>();
        }

        public void Walk()
        {
            ChangeHorizontalSpeedAccordingToInput(walkSpeed);
        }

        public void Run()
        {
            ChangeHorizontalSpeedAccordingToInput(runSpeed);
        }

        public void Idle()
        {
            ChangeHorizontalSpeedAccordingToInput(0);
        }

        private void ChangeHorizontalSpeedAccordingToInput(float speed)
        {
            // var horizontalMovement = inputReader.movementDirection * speed * new Vector2 (transform.forward.x, transform.forward.z);
            LocalMovementDirection =  new Vector3(inputReader.movementDirection.x,0, inputReader.movementDirection.y);
            var worldDirection = transform.TransformDirection(LocalMovementDirection);
            Velocity = worldDirection * speed;
        }
        
        private void Update()
        {
            _characterController.Move( Velocity * Time.deltaTime);
        }
    }
}