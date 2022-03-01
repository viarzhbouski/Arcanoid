using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Platform platform = new Platform();
    private PlatformController platformController;
    
    [SerializeField]
    private PlatformView platformView;

    private void Start()
    {
        platform = new Platform(); 
        platformController = new PlatformController();
        
        platformController.Bind(platformView, platform);
        platformController.Subscribe();
    }

    private void Update()
    {
       //platformController.Move();
    }
}
