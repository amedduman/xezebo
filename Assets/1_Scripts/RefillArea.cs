using System;
using UnityEngine;
using Xezebo.Player;
using Zenject;

public class RefillArea : MonoBehaviour
{
    [Inject] ResourceHandler _resourceHandler;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!CheckIfPlayer(other)) return;
        
        _resourceHandler.StartRefillResources();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CheckIfPlayer(other)) return;
        
        _resourceHandler.StopRefillResources();
    }

    private bool CheckIfPlayer(Collider other)
    {
        return other.TryGetComponent(out PlayerEntity player);
    }
}
