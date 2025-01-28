using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script that fades the a Canvas Renderer  in over the duration of one second.
/// </summary>
public class LogoFadeScript : MonoBehaviour
{
    private float _logoAlpha;
    private CanvasRenderer _canvasRenderer;

    /// <summary>
    /// Unity's builty-in Start method. Gets the CanvasRenderer component and sets the alpha float to 0.0f.
    /// </summary>
    void Start()
    {
        _logoAlpha = 0.0f;
        _canvasRenderer = GetComponent<CanvasRenderer>();
    }

    /// <summary>
    /// Unity's builty-in FixedUpdate method. Increases alpha gradually using deltaTime until it's at 1.0f.
    /// </summary>
    void FixedUpdate()
    {
        if (_logoAlpha < 1.0f)
        {
            _logoAlpha = _logoAlpha + Time.deltaTime;
        }
        else
        {
            _logoAlpha = 1.0f;
        }
        _canvasRenderer.SetAlpha(_logoAlpha);
    }
}
