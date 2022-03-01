using System;

public interface IModel
{
    public void Subscribe(Action onChange);
    public void OnChange();
}
