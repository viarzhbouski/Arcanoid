namespace Scripts.Core.Interfaces.MVC
{
    public interface IView
    {
        public void Bind(IModel model);

        public void RenderChanges();
    }
}