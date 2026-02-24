using Code.Logic.ViewProvider;

namespace Code.Logic.HideViewService
{
    public class HideViewService : IHideViewService
    {
        private readonly IViewProvider _viewProvider;
        
        public HideViewService(IViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
        }

        public void HideAll()
        {
            foreach (var view in _viewProvider.GetViews())
                view.Hide();
        }
    }
}