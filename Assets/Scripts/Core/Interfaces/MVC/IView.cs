namespace Scripts.Core.Interfaces.MVC
{
    public interface IView
    {
        public void Bind(IModel model, IController controller);

        public void RenderChanges();
    }
}