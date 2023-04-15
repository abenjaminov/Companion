using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.ScriptableObjects.Channels
{
    [CreateAssetMenu(menuName = "Channels/Player Channel", fileName = "Player Channel")]
    public class PlayerChannel : ScriptableObject
    {
        public UnityAction OnPlayerJumpAnimationEndEvent;

        public void OnPlayerJumpAnimationEnd()
        {
            OnPlayerJumpAnimationEndEvent?.Invoke();
        }
    }
}