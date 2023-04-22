using System;
using _Scripts.Player.Types;
using _Scripts.Systems.InputSystem;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Player
{
    public class PlayerGameState: MonoBehaviour
    {
        public UnityAction<WeaponType> OnWeaponChangeEvent;
        
        public InputReader inputReader;
        
        public WeaponType CurrentWeaponType = WeaponType.None;

        private void Start()
        {
            inputReader.OnToggleWeaponClickedEvent += OnToggleWeaponClickedEvent;
        }

        private void OnToggleWeaponClickedEvent()
        {
            SetWeaponType(CurrentWeaponType == WeaponType.None ? WeaponType.Rifle : WeaponType.None);
        }

        public void SetWeaponType(WeaponType weaponType)
        {
            CurrentWeaponType = weaponType;
            OnWeaponChangeEvent?.Invoke(CurrentWeaponType);
        }
    }
}