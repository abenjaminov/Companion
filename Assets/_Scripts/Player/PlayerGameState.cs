using _Scripts.Player.Types;
using _Scripts.Systems.InputSystem;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerGameState: MonoBehaviour
    {
        public InputReader inputReader;
        
        [HideInInspector] public WeaponType CurrentWeaponType = WeaponType.None;

        public void SetWeaponType(WeaponType weaponType)
        {
            
        }
    }
}