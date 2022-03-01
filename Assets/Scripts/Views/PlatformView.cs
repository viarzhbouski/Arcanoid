using UnityEngine;
using System;

public class PlatformView : MonoBehaviour, IView
{
    private Action _onChange;

    public void Subscribe(Action onChange)
    {
        _onChange = onChange;
    }
}