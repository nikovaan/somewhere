using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// The script used in the intermission scene to display instructions for the upcoming minigame.
/// </summary>
public class IntermissionTextScript : MonoBehaviour
{
    private TMP_Text _textComponent;
    [SerializeField] private MinigameInstructionsScript _instructionsAsset;
    private string _nextMinigame;
    private float _delayTimer;
    [SerializeField] private string[] _nextMinigameDebug = { "BallCatcher", "BleachBaby", "ButterSpread", "CameraZoom", "CardScene", "DragBabyShapes", "EggHatch", "PetScene", "PourTea", "ShakeScene" };

    /// <summary>
    /// Unity's built-in Start method. Gets a reference to the TMP_Text component and fetches the corresponding play instructions
    /// from the MinigameInstructionsAsset.asset file.
    /// </summary>
    void Start()
    {
        try
        {
            _textComponent = gameObject.GetComponent<TMP_Text>();
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to get TMP component in Start().");
        }
        try
        {
            _nextMinigame = GameManager.Instance.ShuffledMinigames()[GameManager.Instance.MinigamePointer()];
            _textComponent.text = _instructionsAsset.GetInstructions(_nextMinigame);
            //_textComponent.text = _instructionsAsset.GetInstructions(_nextMinigameDebug[0]); // Uncomment this and comment the above two lines if you want to easily debug the text displayed in the intermission screen.
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to get the correct minigame from gamemanager.");
        }
        _delayTimer = 0.0f;
    }

    /// <summary>
    /// Unity's built-in Update method. All it does is check if the player's been in the intermission scene at least 1 second
    /// before letting the player tap the screen to progress. Alternatively, after 5 seconds it goes to next minigame.
    /// </summary>
    void Update()
    {
        if (_delayTimer >= 4.0f)
        {
            GameManager.Instance.LoadMinigame();
        }
        else if(_delayTimer >= 1.0f)
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                try
                {
                    GameManager.Instance.LoadMinigame();
                }
                catch (System.Exception)
                {
                    Debug.Log("Failed to load next minigame.");
                }
            }
        }
    }

    /// <summary>
    /// Unity's built-in FixedUpdate method. All it's used for is advancing _delayTimer by deltaTime per frame until it's
    /// 5.0f, not that it'll ever get there ideally.
    /// </summary>
    private void FixedUpdate()
    {
        if (_delayTimer < 5.0f)
        {
            _delayTimer = _delayTimer + Time.deltaTime;
        }
        else
        {
            _delayTimer = 5.0f;
        }
    }
}
