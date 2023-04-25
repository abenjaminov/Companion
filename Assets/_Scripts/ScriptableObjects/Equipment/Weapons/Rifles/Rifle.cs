using UnityEngine;

namespace _Scripts.ScriptableObjects.Equipment.Weapons.Rifles
{
    [CreateAssetMenu(menuName = "Equipment/Weapons/Rifle")]
    public class Rifle : Weapon
    {
        public int FireRate;
        public int Damage;
    }
}