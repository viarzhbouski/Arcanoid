using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Platform platform = new Platform();
    
    [SerializeField]
    private PlatformView platformView;

    private void Start()
    {
        platform = new Platform();
        var platformController = new PlatformController();
        
        platformController.Bind(platformView, platform);
        platformController.Subscribe();
    }

    private void Update()
    {
        
    }
}
