using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for all the objects that fall down in the Ball Catcher minigame.
/// </summary>
public class ObjectTypeScript_BallCatcher : MonoBehaviour
{
    [SerializeField] public int ObjectType;
    private SpriteRenderer _objectSpriteRenderer;
    [SerializeField] private Sprite[] _trashSprites;
    private int _randomTrashSprite;
    [SerializeField] private bool b_gameEnd, b_gameEndConfirm;
    [SerializeField] private float _gameEndTimer;

    /// <summary>
    /// Unity's built-in OnEnable method. This resets b_gameEnd and _gameEndTimer to deal with a pause-related bug.
    /// It introduces a different bug with pausing though. I'll fix it later if we continue this project.
    /// </summary>
    private void OnEnable()
    {
        if (b_gameEnd == true)
        {
            b_gameEnd = false;
            _gameEndTimer = 0.0f;
        }
    }

    /// <summary>
    /// Unity's built-in Start method. Primarily this checks which object type was created and sets a few variables based on that. Also picks a random trash sprite for trash objects.
    /// </summary>
    void Start()
    {
        _gameEndTimer = 0.0f;
        if (gameObject.name == "TrashPrefab_BallCatcher(Clone)")
        {
            _objectSpriteRenderer = GetComponent<SpriteRenderer>();
            _randomTrashSprite = Random.Range(0, 3);
            _objectSpriteRenderer.sprite = _trashSprites[_randomTrashSprite];
            ObjectType = 0;
        }
        else if (gameObject.name == "BallPrefab_BallCatcher(Clone)")
        {
            ObjectType = 1;
            b_gameEnd = false;
            b_gameEndConfirm = false;
        }
        else
        {
            Debug.Log("Object start function didn't get the right name");
            Debug.Log(gameObject.name);
        }
    }

    /// <summary>
    /// Unity's built-in Update method. It's here entirely because I couldn't think of a better way to check if the player didn't catch anything.
    /// </summary>
    void Update()
    {
        if (b_gameEnd == true && _gameEndTimer < 6.0f)
        {
            _gameEndTimer = _gameEndTimer + Time.deltaTime;
        }
        else if (b_gameEnd == true && _gameEndTimer > 6.0f)
        {
            _gameEndTimer = 6.0f;
        }

        if (b_gameEnd == true && b_gameEndConfirm == false && _gameEndTimer >= 6.0f)
        {
            try
            {
                b_gameEndConfirm = true;
                GameManager.Instance.UnloadMinigame(false);
            }
            catch (System.Exception)
            {
                Debug.Log("Couldn't find game manager instance, game is now softlocked.");
            }
        }
        else if (b_gameEnd == true && b_gameEndConfirm == false && _gameEndTimer >= 1.0f)
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                try
                {
                    b_gameEndConfirm = true;
                    GameManager.Instance.UnloadMinigame(false);
                }
                catch (System.Exception)
                {
                    Debug.Log("Couldn't find game manager instance, game is now softlocked.");
                }
            }
        }
    }

    /// <summary>
    /// Checks if the object is the catch target and if so, sets a boolean that the game has ended.
    /// </summary>
    private void OnBecameInvisible()
    {
        if (ObjectType == 1)
        {
            b_gameEnd = true;
        }
    }
}
