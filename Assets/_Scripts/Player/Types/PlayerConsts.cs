using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Player.Types
{
    public static class PlayerConsts
    {
        public static readonly Dictionary<WeaponType, Dictionary<StateType, int>> WeaponTypeToStateTypeToAnimationHash = new()
        {
            {
                WeaponType.None, new Dictionary<StateType, int>
                {
                    { StateType.Idle, Animator.StringToHash("Idle") },
                    { StateType.Walk, Animator.StringToHash("Walk") },
                    { StateType.WalkStrafeLeft, Animator.StringToHash("WalkStrafeLeft") },
                    { StateType.WalkStrafeRight, Animator.StringToHash("WalkStrafeRight") },
                    { StateType.WalkBack, Animator.StringToHash("WalkBack") },
                    { StateType.WalkStrafeLeftBack, Animator.StringToHash("WalkStrafeLeftBack") },
                    { StateType.WalkStrafeRightBack, Animator.StringToHash("WalkStrafeRightBack") },
                    { StateType.Run, Animator.StringToHash("Run")},
                    { StateType.RunStrafeLeft, Animator.StringToHash("RunStrafeLeft") },
                    { StateType.RunStrafeRight, Animator.StringToHash("RunStrafeRight") },
                    { StateType.RunBack, Animator.StringToHash("RunBack") },
                    { StateType.RunStrafeLeftBack, Animator.StringToHash("RunStrafeLeftBack") },
                    { StateType.RunStrafeRightBack, Animator.StringToHash("RunStrafeRightBack") },
                    { StateType.Jump, Animator.StringToHash("Jump") },
                }
            },
            {
                WeaponType.Rifle, new Dictionary<StateType, int>
                {
                    { StateType.Idle, Animator.StringToHash("IdleRifle") },
                    { StateType.Walk, Animator.StringToHash("WalkRifle") },
                    { StateType.WalkStrafeLeft, Animator.StringToHash("WalkRifle") },
                    { StateType.WalkStrafeRight, Animator.StringToHash("WalkRifle") },
                    { StateType.WalkBack, Animator.StringToHash("WalkRifleBack") },
                    { StateType.WalkStrafeLeftBack, Animator.StringToHash("WalkRifleBack") },
                    { StateType.WalkStrafeRightBack, Animator.StringToHash("WalkRifleBack") },
                    { StateType.Run, Animator.StringToHash("RunRifle")},
                    { StateType.RunStrafeLeft, Animator.StringToHash("RunRifle") },
                    { StateType.RunStrafeRight, Animator.StringToHash("RunRifle") },
                    { StateType.RunBack, Animator.StringToHash("RunRifle") },
                    { StateType.RunStrafeLeftBack, Animator.StringToHash("RunRifle") },
                    { StateType.RunStrafeRightBack, Animator.StringToHash("RunRifle") },
                    { StateType.Jump, Animator.StringToHash("Jump") },
                }
            }
        };
        
        public static readonly Dictionary<WeaponType, Dictionary<StateType, int>> AimingWeaponTypeToStateTypeToAnimationHash = new()
        {
            {
                WeaponType.Rifle, new Dictionary<StateType, int>
                {
                    { StateType.Idle, Animator.StringToHash("IdleRifleAim") },
                    { StateType.Walk, Animator.StringToHash("WalkRifleAim") },
                    { StateType.WalkStrafeLeft, Animator.StringToHash("WalkRifleAim") },
                    { StateType.WalkStrafeRight, Animator.StringToHash("WalkRifleAim") },
                    { StateType.WalkBack, Animator.StringToHash("WalkRifleAimBack") },
                    { StateType.WalkStrafeLeftBack, Animator.StringToHash("WalkRifleAimBack") },
                    { StateType.WalkStrafeRightBack, Animator.StringToHash("WalkRifleAimBack") },
                    { StateType.Run, Animator.StringToHash("RunRifleAim")},
                    { StateType.RunStrafeLeft, Animator.StringToHash("RunRifleAim") },
                    { StateType.RunStrafeRight, Animator.StringToHash("RunRifleAim") },
                    { StateType.RunBack, Animator.StringToHash("RunRifleAim") },
                    { StateType.RunStrafeLeftBack, Animator.StringToHash("RunRifleAim") },
                    { StateType.RunStrafeRightBack, Animator.StringToHash("RunRifleAim") },
                    { StateType.Jump, Animator.StringToHash("Jump") },
                }
            }
        };
    }
}