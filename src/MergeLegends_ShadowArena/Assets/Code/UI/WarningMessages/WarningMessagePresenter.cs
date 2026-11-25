namespace Code.UI.WarningMessages
{
    public class WarningMessagePresenter : Presenter<WarningMessageView>
    {
        private readonly string _message;
        
        public WarningMessagePresenter(WarningMessageView view, string message) : base(view)
        {
            _message = message;
        }

        public override void Initialize() 
            => View.SetValue(_message);
    }
}