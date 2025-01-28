using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Initialiser script when launching the game. 
/// </summary>
public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject GameManagerObject;
    [SerializeField] private GameObject AudioManagerObject;
    [SerializeField] private Camera MainCameraObject;
    [SerializeField] private CamAdjust _mainCameraScript;
    private string _sceneName;
    private float _countdown;

    /// <summary>
    /// Unity's built-in Start method. Instantiates the game manager and audio manager singletons, passes game manager a few values, checks for a save to load.
    /// </summary>
    void Start()
    {
        Instantiate(GameManagerObject);
        Instantiate(AudioManagerObject);
        GameManager.Instance.SetMainCameraObject(MainCameraObject);
        GameManager.Instance.SetMainCameraScript(_mainCameraScript);
        _sceneName = "TitleScreen";
        try
        {
            _sceneName = GameManager.Instance.LoadGame();
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to load save via GameManager");
        }
        _countdown = 5.0f;
    }

    /// <summary>
    /// Unity's built-in Update method. Waits for input before progressing to either the title screen or the interrupted run. Except no save is loaded atm.
    /// </summary>
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(_sceneName);
        }
    }

    /// <summary>
    /// Unity's built-in FixedUpdate method. Tics _countdown down by deltaTime until it's 0 and then goes to next scene even if player doesn't tap the screen.
    /// </summary>
    void FixedUpdate()
    {
        if (_countdown > 0.0f)
        {
            _countdown = _countdown - Time.deltaTime;
        }
        else
        {
            _countdown = 0.0f;
            SceneManager.LoadScene(_sceneName);
        }
    }
}
