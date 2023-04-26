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
        public PlayerEquipment PlayerEquipment;
        public InputReader inputReader;

        [SerializeField] private Transform RiflePosition;
        private GameObject _rifleInstance;
        
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
            if (weaponType == WeaponType.Rifle)
            {
                _rifleInstance = Instantiate(PlayerEquipment.WeaponSlot1.WeaponPrefab, RiflePosition);
            }
            else if (weaponType == WeaponType.None && _rifleInstance != null)
            {
                Destroy(_rifleInstance.gameObject);
                _rifleInstance = null;
            }
            
            OnWeaponChangeEvent?.Invoke(CurrentWeaponType);
        }
    }
}