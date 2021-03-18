using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject background;

    public Material[] backgroundImages = new Material[3];

    private int _currentBackground = 1;

    private Renderer _renderer;
    // Start is called before the first frame update
    private void Start()
    {
        _renderer = background.GetComponent<Renderer>();
    }
    
    private void LateUpdate()
    {
        _renderer.material.mainTextureOffset = new Vector2(Time.time * Settings.BackgroundSpeed, 0);
        if (Settings.TimesIncreased % 10 != 0) return;
        
        Settings.TimesIncreased++;
        _renderer.material = backgroundImages[_currentBackground++];
        _currentBackground %= 3;
        Settings.BackgroundSpeed *=2;
    }
    
}
