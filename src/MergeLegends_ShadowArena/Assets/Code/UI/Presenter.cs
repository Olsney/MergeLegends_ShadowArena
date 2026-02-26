using System;
using Zenject;

namespace Code.UI
{
    public abstract class Presenter<T> : IInitializable, IDisposable
    {
        protected Presenter(T view)
        {
            View = view;
        }

        protected T View { get; }

        public abstract void Initialize();

        public abstract void Dispose();
    }
}