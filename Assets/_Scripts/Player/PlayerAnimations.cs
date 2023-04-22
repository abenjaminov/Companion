using System;
using System.Collections.Generic;
using _Scripts.Player.Types;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private PlayerGameState PlayerGameState;
        [SerializeField] private Animator animator;

        private Dictionary<int, int> animatorLayerIndexToStateHash = new();

        private int _baseLayerIndex;
        private int _upperBodyLayerIndex;
        
        private void Start()
        {
            PlayerGameState.OnWeaponChangeEvent += OnWeaponChangeEvent;
            _baseLayerIndex = animator.GetLayerIndex("Base Layer");
            _upperBodyLayerIndex = animator.GetLayerIndex("Upper Body");
            
            animatorLayerIndexToStateHash.Add(_baseLayerIndex, -1);
            animatorLayerIndexToStateHash.Add(_upperBodyLayerIndex, -1);
        }

        private void OnWeaponChangeEvent(WeaponType weaponType)
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Upper Body"), weaponType == WeaponType.None ? 0 : 1);
        }

        public void UpdateAnimations(StateType stateType)
        {
            var baseStateHash =
                PlayerConsts.WeaponTypeToStateTypeToAnimationHash[WeaponType.None][stateType];

            if (baseStateHash != animatorLayerIndexToStateHash[_baseLayerIndex])
            {
                animator.CrossFade(baseStateHash, 0, _baseLayerIndex);
                animatorLayerIndexToStateHash[_baseLayerIndex] = baseStateHash;
            }
            

            if (PlayerGameState.CurrentWeaponType == WeaponType.None) return;
            
            var upperBodyStateHash =
                PlayerConsts.WeaponTypeToStateTypeToAnimationHash[PlayerGameState.CurrentWeaponType][stateType];

            if (animatorLayerIndexToStateHash[_upperBodyLayerIndex] == upperBodyStateHash) return;
            
            animator.CrossFade(upperBodyStateHash, 0, _upperBodyLayerIndex);
            animatorLayerIndexToStateHash[_upperBodyLayerIndex] = upperBodyStateHash;
        }
    }
}