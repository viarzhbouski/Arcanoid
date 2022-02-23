public class PlatformController : IController
{
    private IView _view;
    private IModel _model;

    public void Bind(IView view, IModel model)
    {
        _view = view;
        _model = model;
    }
}