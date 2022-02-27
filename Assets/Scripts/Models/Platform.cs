using System;

public class Platform : IModel
{
    private Action _onChange;

    public void Subscribe(Action onChange)
    {
        _onChange = onChange;
    }

    public void OnChange()
    {
        _onChange?.Invoke();
    }
}