using _Scripts.ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts
{
    public class PlayerAnimationsEventHandler : MonoBehaviour
    {
        [SerializeField] private PlayerChannel playerChannel;
        
        public void OnJumpAnimationEnd()
        {
            playerChannel.OnPlayerJumpAnimationEnd();
        }

        public void OnGrabRifleEnd()
        {
            
        }
    }
}