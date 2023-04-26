using _Scripts.Systems.InputSystem;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerAim: MonoBehaviour
    {
        [SerializeField] private InputReader InputReader;

        [SerializeField] private GameObject MainCamera;
        // [SerializeField] private GameObject MoveCamera;
        [SerializeField] private GameObject AimCamera;
        
        void Update()
        {
            if (InputReader.IsAiming && !AimCamera.activeInHierarchy)
            {
                MainCamera.SetActive(false);
                AimCamera.SetActive(true);

                //Allow time for the camera to blend before enabling the UI
                //StartCoroutine(ShowReticle());
            }
            else if(!InputReader.IsAiming && !MainCamera.activeInHierarchy)
            {
                MainCamera.SetActive(true);
                AimCamera.SetActive(false);
                //aimReticle.SetActive(false);
            }
        
        }
    }
}