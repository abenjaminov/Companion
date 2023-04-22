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

        public void SetWeaponType(WeaponType weaponType)
        {
            CurrentWeaponType = weaponType;
            OnWeaponChangeEvent?.Invoke(CurrentWeaponType);
        }
    }
}