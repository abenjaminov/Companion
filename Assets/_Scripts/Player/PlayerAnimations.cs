using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Player.Types;
using _Scripts.Systems.InputSystem;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private PlayerGameState PlayerGameState;
        [SerializeField] private Animator Animator;
        [SerializeField] private InputReader InputReader;

        private List<AnimationPredicate> _animationPredicates = new();
        
        private void Start()
        {
            foreach (var key in PlayerConsts.WeaponTypeToStateTypeToAnimationHash[WeaponType.None].Keys)
            {
                _animationPredicates.Add(new AnimationPredicate()
                {
                    AnimationHash = PlayerConsts.WeaponTypeToStateTypeToAnimationHash[WeaponType.None][key],
                    Predicate = (stateType) => !InputReader.IsAiming && PlayerGameState.CurrentWeaponType == WeaponType.None && stateType == key
                });
            }
            
            foreach (var key in PlayerConsts.WeaponTypeToStateTypeToAnimationHash[WeaponType.Rifle].Keys)
            {
                _animationPredicates.Add(new AnimationPredicate()
                {
                    AnimationHash = PlayerConsts.WeaponTypeToStateTypeToAnimationHash[WeaponType.Rifle][key],
                    Predicate = (stateType) => !InputReader.IsAiming && PlayerGameState.CurrentWeaponType == WeaponType.Rifle && stateType == key
                });
            }
            
            foreach (var key in PlayerConsts.AimingWeaponTypeToStateTypeToAnimationHash[WeaponType.Rifle].Keys)
            {
                _animationPredicates.Add(new AnimationPredicate()
                {
                    AnimationHash = PlayerConsts.AimingWeaponTypeToStateTypeToAnimationHash[WeaponType.Rifle][key],
                    Predicate = (stateType) => InputReader.IsAiming && PlayerGameState.CurrentWeaponType == WeaponType.Rifle && stateType == key
                });
            }
        }

        public void UpdateAnimations(StateType stateType)
        {
            var index = _animationPredicates.FindIndex(x => x.Predicate(stateType));

            if (index == -1) return;
            
            Animator.CrossFade(_animationPredicates[index].AnimationHash, 0, 0);
        }

        private struct AnimationPredicate
        {
            public Func<StateType, bool> Predicate;
            public int AnimationHash;
        }
    }
}