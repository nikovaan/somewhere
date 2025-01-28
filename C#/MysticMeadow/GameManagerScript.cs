using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Manager is a singleton that gets references to several commonly referred to game objects and stores them for ease of use.
/// It also keeps track of current movement and interact modes and whether or not the game is paused or dialogue is active.
/// </summary>
public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }

    public GameObject PlayerObjectObject;
    public Camera MainCameraObject;
    public bool IsGamePaused, IsDialogueActive;
    public MovementMode CurrentMovementMode;
    public InteractMode CurrentInteractMode;

    /// <summary>
    /// Initialization done in Awake so that it runs before most other things. In the future I will not be using Unity's built in
    /// initialization methods in order to avoid order of execution bugs.
    /// I should also make the error handling more robust than this, but for now this will do.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("For some reason there was a duplicate Game Manager?");
            Destroy(this);
        }
        try
        {
            PlayerObjectObject = GameObject.Find("PlayerObject");
        }
        catch
        {
            Debug.Log("GameManagerScript failed to find PlayerObject");
        }
        try
        {
            MainCameraObject = Camera.main;
        }
        catch
        {
            Debug.Log("GameManagerScript failed to find Main Camera");
        }
        IsDialogueActive = false;
    }
}
