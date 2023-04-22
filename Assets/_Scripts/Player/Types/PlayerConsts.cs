using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Player.Types
{
    public static class PlayerConsts
    {
        public static Dictionary<WeaponType, Dictionary<StateType, int>> WeaponTypeToStateTypeToAnimationHash = new()
        {
            {
                WeaponType.None, new Dictionary<StateType, int>
                {
                    { StateType.Idle, Animator.StringToHash("Idle") },
                    { StateType.Walk, Animator.StringToHash("Walk") },
                    { StateType.WalkStrafeLeft, Animator.StringToHash("Walk Strafe Left") },
                    { StateType.WalkStrafeRight, Animator.StringToHash("Walk Strafe Right") },
                    { StateType.WalkBack, Animator.StringToHash("Walk Back") },
                    { StateType.WalkStrafeLeftBack, Animator.StringToHash("Walk Strafe Left Back") },
                    { StateType.WalkStrafeRightBack, Animator.StringToHash("Walk Strafe Right Back") },
                    { StateType.Run, Animator.StringToHash("Run")},
                    { StateType.RunStrafeLeft, Animator.StringToHash("Run Strafe Left") },
                    { StateType.RunStrafeRight, Animator.StringToHash("Run Strafe Right") },
                    { StateType.RunBack, Animator.StringToHash("Run Back") },
                    { StateType.RunStrafeLeftBack, Animator.StringToHash("Run Strafe Left Back") },
                    { StateType.RunStrafeRightBack, Animator.StringToHash("Run Strafe Right Back") },
                    { StateType.Jump, Animator.StringToHash("Jump") },
                }
            },
            {
                WeaponType.Rifle, new Dictionary<StateType, int>
                {
                    { StateType.Idle, Animator.StringToHash("IdleRifle") },
                    { StateType.Walk, Animator.StringToHash("WalkRifle") },
                    { StateType.WalkStrafeLeft, Animator.StringToHash("Walk Strafe Left") },
                    { StateType.WalkStrafeRight, Animator.StringToHash("Walk Strafe Right") },
                    { StateType.WalkBack, Animator.StringToHash("Walk Back") },
                    { StateType.WalkStrafeLeftBack, Animator.StringToHash("Walk Strafe Left Back") },
                    { StateType.WalkStrafeRightBack, Animator.StringToHash("Walk Strafe Right Back") },
                    { StateType.Run, Animator.StringToHash("Run")},
                    { StateType.RunStrafeLeft, Animator.StringToHash("Run Strafe Left") },
                    { StateType.RunStrafeRight, Animator.StringToHash("Run Strafe Right") },
                    { StateType.RunBack, Animator.StringToHash("Run Back") },
                    { StateType.RunStrafeLeftBack, Animator.StringToHash("Run Strafe Left Back") },
                    { StateType.RunStrafeRightBack, Animator.StringToHash("Run Strafe Right Back") },
                    { StateType.Jump, Animator.StringToHash("Jump") },
                }
            }
        };
    }
}