using System;

public interface IView
{
    public void Subscribe(Action onChange);
}