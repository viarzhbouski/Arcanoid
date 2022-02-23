using System;

public class Platform : IModel
{
    private Action _onChange;

    public Platform()
    {
        
    }

    public void Subscribe(Action onChange)
    {
        _onChange = onChange;
    }

    public void Change()
    {
        _onChange?.Invoke();
    }
}