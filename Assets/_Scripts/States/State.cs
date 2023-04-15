using Unity.VisualScripting;

namespace _Scripts.States
{
    public abstract class State
    {
        public abstract void OnEnter();
        public abstract void OnExit();
    }
}