using System.Collections.Generic;
using Code.UI.View;

namespace Code.Logic.ViewProvider
{
    public interface IViewProvider
    {
        void AddView(IView view);
        List<IView> GetViews();
    }
}