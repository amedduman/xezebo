using System;
using UnityEngine;
using Xezebo.Player;
using Zenject;

public class RefillArea : MonoBehaviour
{
    [Inject] ResourceHandler _resourceHandler;

    [SerializeField] bool _isTutorial;
    [SerializeField] AudioSource _themeMusic;
    [SerializeField] AudioSource _refillAreaMusic;

    bool _firstTimeRefillSound = true;
    bool _firstTimeThemeMusic = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!CheckIfPlayer(other)) return;
        
        _resourceHandler.StartRefillResources();

        if(_isTutorial) return;

        _themeMusic.Pause();

        if(_firstTimeRefillSound)
        {
            _refillAreaMusic.Play();
        }
        else
        {
            _refillAreaMusic.UnPause();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CheckIfPlayer(other)) return;
        
        _resourceHandler.StopRefillResources();

        if(_isTutorial) return;

        _refillAreaMusic.Pause();

        if(_firstTimeThemeMusic)
        {
            _themeMusic.Play();
        }
        else
        {
            _themeMusic.UnPause();
        }
    }

    private bool CheckIfPlayer(Collider other)
    {
        return other.TryGetComponent(out PlayerEntity player);
    }
}
