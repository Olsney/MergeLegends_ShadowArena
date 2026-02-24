using System.Collections.Generic;
using Code.UI.View;

namespace Code.Logic.ViewProvider
{
    public class ViewProvider : IViewProvider
    {
        private readonly List<IView> _views = new List<IView>();

        public void AddView(IView view)
        {
            _views.Add(view);
        }
        
        public List<IView> GetViews()
        {
            return _views;
        }
    }
}