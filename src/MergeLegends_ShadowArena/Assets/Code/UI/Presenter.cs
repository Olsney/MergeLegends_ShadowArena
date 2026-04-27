using System;
using Zenject;

namespace Code.UI
{
    public abstract class Presenter<IView> : IInitializable, IDisposable
    {
        protected Presenter(IView view)
        {
            View = view;
        }

        protected IView View { get; }

        public abstract void Initialize();

        public virtual void Dispose()
        {
        }
    }
}