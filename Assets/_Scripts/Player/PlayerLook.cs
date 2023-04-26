using System;
using _Scripts.Systems.InputSystem;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private InputReader InputReader;
        [SerializeField] private Transform PlayerFollowTransform;

        [SerializeField] private float RotationPower = 3f;

        [SerializeField] private float RotationLerpSpeed = 0.5f;
        [SerializeField] private Quaternion NextRotation;
        private void Update()
        {
            var followTransform = PlayerFollowTransform.transform;
            var localEulerAngles = followTransform.localEulerAngles;
            
            PlayerFollowTransform.transform.rotation *=
                Quaternion.AngleAxis(-1 * InputReader.MouseDelta.x * RotationPower, Vector3.up);
            followTransform.rotation *= Quaternion.AngleAxis(InputReader.MouseDelta.y * RotationPower, Vector3.right);
            
            var angles = followTransform.localEulerAngles;
            angles.z = 0;

            var angle = localEulerAngles.x;

            //Clamp the Up/Down rotation
            if (angle is > 180 and < 340)
            {
                angles.x = 340;
            }
            else if(angle is < 180 and > 40)
            {
                angles.x = 40;
            }

            PlayerFollowTransform.transform.localEulerAngles = angles;
            
            NextRotation = Quaternion.Lerp(followTransform.transform.rotation, NextRotation, Time.deltaTime * RotationLerpSpeed);

            if (InputReader.movementDirection.x == 0 && InputReader.movementDirection.y == 0) 
            {
                if (InputReader.IsAiming)
                {
                    //Set the player rotation based on the look transform
                    transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
                    //reset the y rotation of the look transform
                    followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
                }

                return; 
            }
        

            //Set the player rotation based on the look transform
            transform.rotation = Quaternion.Euler(0, followTransform.transform.rotation.eulerAngles.y, 0);
            //reset the y rotation of the look transform
            followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
        }
    }
}