using System;

namespace Spawnable
{
    public interface ISpawnable<out T>
    {
        void Reset();
        void Release();

        event Action<T> Disappeared;
    }
}