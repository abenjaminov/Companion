using System;

namespace _Scripts
{
    public struct MapThreadInfo<T>
    {
        public readonly Action<T> Callback;
        public readonly T Parameter;

        public MapThreadInfo(T parameter, Action<T> callback)
        {
            Parameter = parameter;
            Callback = callback;
        }
    }
}