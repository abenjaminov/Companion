using System;
using System.Collections;
using _Scripts.Systems.InputSystem;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class Rifle : MonoBehaviour
    {
        [SerializeField] private ScriptableObjects.Equipment.Weapons.Rifles.Rifle RifleInfo;
        private InputReader _inputReader;

        private float _timeBetweenShotsInSeconds;
        
        private void Awake()
        {
            _inputReader = FindObjectOfType<InputReader>();
            _inputReader.OnShootStartEvent += OnShootStartEvent;
        }

        private void OnShootStartEvent()
        {
            _timeBetweenShotsInSeconds = 60f / RifleInfo.FireRate;
            StartCoroutine(nameof(ShootInterval));
        }

        private IEnumerator ShootInterval()
        {
            while (_inputReader.IsShooting)
            {
                Shoot();
            
                yield return new WaitForSeconds(_timeBetweenShotsInSeconds); 
            }
        }

        private void Shoot()
        {
            var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            
            if (!Physics.Raycast(ray, out var hit)) return;
            
            Debug.Log("Hit " + hit.collider.name);
        }
    }
}