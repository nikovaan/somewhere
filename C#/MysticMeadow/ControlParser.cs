using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ControlParser handles most of player's actual functionality. _castFilter has been serialized entirely for ease of checking things in editor.
/// </summary>
public class ControlParser : MonoBehaviour
{
    public bool MoveLeft, MoveRight, MoveUp, MoveDown, Interact, Pause;
    public Vector2 PlayerVelocity;
    [SerializeField] private ContactFilter2D _castFilter;
    private Rigidbody2D _rb2D, _sokobanObject;
    private Collider2D _castCollider;
    private PlayerScript _playerScript;
    private SokobanScript _sokobanScript;
    private PlayerPickupScript _pickupScript;
    private int _moveLeftTimer, _moveRightTimer, _moveUpTimer, _moveDownTimer, _sokobanCast, _interactTimer, _pauseTimer;
    private Vector2 _cameraWorldMax, _cameraWorldMin, _castDirection;
    private RaycastHit2D[] _castResults;

    /// <summary>
    /// This method parses player's inputs from Input Manager and stores them in various booleans for later use.
    /// </summary>
    public void InputHandler()
    {
        if (Input.GetAxisRaw("Left") > 0)
        {
            MoveLeft = true;
        }
        if (Input.GetAxisRaw("Right") > 0)
        {
            MoveRight = true;
        }
        if (Input.GetAxisRaw("Down") > 0)
        {
            MoveDown = true;
        }
        if (Input.GetAxisRaw("Up") > 0)
        {
            MoveUp = true;
        }
        if (Input.GetAxisRaw("Interact") > 0)
        {
            Interact = true;
        }
        if (Input.GetAxisRaw("Pause") > 0)
        {
            Pause = true;
        }
    }

    /// <summary>
    /// This method uses the inputs parsed by InputHandler and runs some checks for mutually exclusive inputs before then actually implementing said inputs.
    /// </summary>
    public void PlayerControls()
    {
        if (MoveLeft == true && MoveRight == true)
        {
            if (_moveLeftTimer > _moveRightTimer)
            {
                PlayerVelocity.x = -1;
                _moveLeftTimer = _moveLeftTimer + 1;
                _moveRightTimer = _moveRightTimer + 1;
            }
            else if (_moveLeftTimer < _moveRightTimer)
            {
                PlayerVelocity.x = 1;
                _moveLeftTimer = _moveLeftTimer + 1;
                _moveRightTimer = _moveRightTimer + 1;
            }
            else if (_moveLeftTimer == _moveRightTimer)
            {
                PlayerVelocity.x = 1;
                _moveRightTimer = _moveRightTimer + 1;
            }
            MoveLeft = false;
            MoveRight = false;
        }
        else if (MoveLeft == true && MoveRight == false)
        {
            PlayerVelocity.x = -1;
            _moveLeftTimer = _moveLeftTimer + 1;
            _moveRightTimer = 0;
            MoveLeft = false;
        }
        else if (MoveLeft == false && MoveRight == true)
        {
            PlayerVelocity.x = 1;
            _moveLeftTimer = 0;
            _moveRightTimer = _moveRightTimer + 1;
            MoveRight = false;
        }
        else if (MoveLeft == false && MoveRight == false)
        {
            _moveLeftTimer = 0;
            _moveRightTimer = 0;
        }
        else
        {
            // I don't see how we ever get here but JUST IN CASE.
            Debug.Log("something went wrong with left and right controls code");
        }
        if (MoveUp == true && MoveDown == true)
        {
            if (_moveUpTimer > _moveDownTimer)
            {
                PlayerVelocity.y = 1;
                _moveUpTimer = _moveUpTimer + 1;
                _moveDownTimer = _moveDownTimer + 1;
            }
            else if (_moveUpTimer < _moveDownTimer)
            {
                PlayerVelocity.y = -1;
                _moveUpTimer = _moveUpTimer + 1;
                _moveDownTimer = _moveDownTimer + 1;
            }
            else if (_moveLeftTimer == _moveRightTimer)
            {
                PlayerVelocity.y = 1;
                _moveUpTimer = _moveUpTimer + 1;
            }
            MoveUp = false;
            MoveDown = false;
        }
        else if (MoveUp == true && MoveDown == false)
        {
            PlayerVelocity.y = 1;
            _moveUpTimer = _moveUpTimer + 1;
            _moveDownTimer = 0;
            MoveUp = false;
        }
        else if (MoveUp == false && MoveDown == true)
        {
            PlayerVelocity.y = -1;
            _moveUpTimer = 0;
            _moveDownTimer = _moveDownTimer + 1;
            MoveDown = false;
        }
        else if (MoveUp == false && MoveDown == false)
        {
            _moveUpTimer = 0;
            _moveDownTimer = 0;
        }
        else
        {
            // I don't see how we ever get here but JUST IN CASE.
            Debug.Log("something went wrong with up and down controls code");
        }
        if (Interact == false && _interactTimer > 0)
        {
            _interactTimer = 0;
        }
        else if (Interact == true && _interactTimer > 0)
        {
            Interact = false;
        }
        else if (Interact == true && _interactTimer <= 0)
        {
            _interactTimer = 6;
            Interact = false;
            InteractPicker();
        }
        if (Pause == true && GameManagerScript.Instance.IsGamePaused == false && _pauseTimer <= 0)
        {
            GameManagerScript.Instance.IsGamePaused = true;
            GameManagerScript.Instance.CurrentMovementMode = MovementMode.None;
            GameManagerScript.Instance.CurrentInteractMode = InteractMode.Pause;
            UIManagerScript.Instance.PauseMenu.SetActive(true);
            _pauseTimer = 6;
            Pause = false;
        }
        else if (Pause == true && GameManagerScript.Instance.IsGamePaused == true && _pauseTimer <= 0)
        {
            GameManagerScript.Instance.IsGamePaused = false;
            GameManagerScript.Instance.CurrentMovementMode = MovementMode.Normal;
            GameManagerScript.Instance.CurrentInteractMode = InteractMode.None;
            UIManagerScript.Instance.PauseMenu.SetActive(false);
            _pauseTimer = 6;
            Pause = false;
        }
        else if (Pause == true && _pauseTimer > 0)
        {
            Pause = false;
        }
        else if (Pause == false && _pauseTimer > 0)
        {
            _pauseTimer = 0;
        }
    }

    //This was meant to be a generic SOCD cleaner method but I got sidetracked before finishing it.
    //public int[] SOCDCleaner(bool _flag1, bool _flag2, int _timer1, int _timer2)
    //{
    //    _socdResults.
    //    if (_flag1 == true && _flag2 == true)
    //    {
    //        if (_timer1 > _timer2)
    //        {
    //            _socdResults[0] = -1;
    //            _socdResults[1] = _timer1 + 1;
    //            _socdResults[2] = _timer2 + 1;
    //        }
    //        else if (_timer1 < _timer2)
    //        {
    //            _socdResults[0] = 1;
    //            _socdResults[1] = _timer1 + 1;
    //            _socdResults[2] = _timer2 + 1;
    //        }
    //        else if (_timer1 == _timer2)
    //        {
    //            _socdResults[0] = 1;
    //            _socdResults[1] = _timer1;
    //            _socdResults[2] = _timer2 + 1;
    //        }
    //    }
    //    else if (_flag1 == true && _flag2 == false)
    //    {
    //        _socdResults[0] = -1;
    //        _socdResults[1] = _timer1 + 1;
    //        _socdResults[2] = 0;
    //    }
    //    else if (_flag1 == false && _flag2 == true)
    //    {
    //        _socdResults[0] = 1;
    //        _socdResults[1] = 0;
    //        _socdResults[2] = _timer2 + 1;
    //    }
    //    else if (_flag1 == false && _flag2 == false)
    //    {
    //        _socdResults[1] = 0;
    //        _socdResults[2] = 0;
    //    }
    //    else
    //    {
    //        // I don't see how we ever get here but JUST IN CASE.
    //        Debug.Log("something went wrong with SOCDCleaner code");
    //    }
    //    return _socdResults;
    //}

    /// <summary>
    /// Normalizes movement values so that we don't get SR40 in the year 2024. Also make sure player can't escape visible camera bounds, hopefully.
    /// References to MovementMode are used to check player's current state for setting an appropriate speed multiplier.
    /// </summary>
    public void NormalizeMovement()
    {
        _cameraWorldMax = GameManagerScript.Instance.MainCameraObject.ViewportToWorldPoint(new Vector3(0.95f, 0.95f, 10.00f));
        _cameraWorldMin = GameManagerScript.Instance.MainCameraObject.ViewportToWorldPoint(new Vector3(0.05f, 0.05f, 10.00f));
        PlayerVelocity.Normalize();
        if (PlayerVelocity != Vector2.zero)
        {
            _castDirection = PlayerVelocity;
        }
        if (_rb2D.transform.position.x <= _cameraWorldMin.x)
        {
            PlayerVelocity = new Vector2(Mathf.Clamp(PlayerVelocity.x, 0f, 1f), PlayerVelocity.y);
        }
        if (_rb2D.transform.position.x >= _cameraWorldMax.x)
        {
            PlayerVelocity = new Vector2(Mathf.Clamp(PlayerVelocity.x, -1f, 0f), PlayerVelocity.y);
        }
        if (_rb2D.transform.position.y <= _cameraWorldMin.y)
        {
            PlayerVelocity = new Vector2(PlayerVelocity.x, Mathf.Clamp(PlayerVelocity.y, 0f, 1f));
        }
        if (_rb2D.transform.position.y >= _cameraWorldMax.y)
        {
            PlayerVelocity = new Vector2(PlayerVelocity.x, Mathf.Clamp(PlayerVelocity.y, -1f, 0f));
        }
        if (GameManagerScript.Instance.CurrentMovementMode == MovementMode.Normal)
        {
            _rb2D.velocity = PlayerVelocity * 5;
        }
        else if (GameManagerScript.Instance.CurrentMovementMode == MovementMode.Sokoban)
        {
            _rb2D.velocity = PlayerVelocity * 2;
            _sokobanObject.velocity = _rb2D.velocity;
            _sokobanScript.SokobanDistanceChecker(_rb2D, _sokobanObject);
        }
        else if (GameManagerScript.Instance.CurrentMovementMode == MovementMode.None)
        {
            _rb2D.velocity = PlayerVelocity * 0;
        }
        if (GameManagerScript.Instance.IsGamePaused == true)
        {
            _rb2D.velocity = PlayerVelocity * 0;
        }
        PlayerVelocity = Vector2.zero;
    }

    /// <summary>
    /// Checks what the current Interact mode is and uses it to determine what the Interact button should actually do.
    /// </summary>
    private void InteractPicker()
    {
        switch (GameManagerScript.Instance.CurrentInteractMode)
        {
            case InteractMode.None:
                break;
            case InteractMode.Sokoban:
                SokobanInteract();
                break;
            case InteractMode.Pickup:
                PickupInteract();
                break;
            case InteractMode.Talk:
                TalkInteract();
                break;
            case InteractMode.Pause:
                PausedInteract();
                break;
            default: // We should never arrive here, but JUST IN CASE
                Debug.Log("Somehow InteractPicker arrived to Default case");
                break;
        }
    }

    /// <summary>
    /// SokobanInteract is called when player is near a movable object. It raycasts in player's last moved direction to see if there's anything to move nearby
    /// and calls the Sokoban script if there is. Alternatively, if player was already moving an object, the script stops moving the object and returns
    /// movement mode back to normal.
    /// </summary>
    private void SokobanInteract()
    {
        if (GameManagerScript.Instance.CurrentMovementMode == MovementMode.Sokoban)
        {
            _sokobanScript.EndSokoban(_sokobanObject);
            return;
        }
        _castFilter.SetLayerMask(64);
        _castResults = new RaycastHit2D[3];
        _sokobanCast = Physics2D.Raycast(_rb2D.transform.position, _castDirection, _castFilter, _castResults, 1f);
        Debug.DrawRay(_rb2D.transform.position, _castDirection, Color.white, 3f, false);
        if (_sokobanCast == 0)
        {
            return;
        }
        else
        {
            try
            {
                _castCollider = _castResults[0].collider;
            }
            catch
            {
                Debug.Log("SokobanInteract Raycast found a collider but it wasn't a collider...?");
                return;
            }
            if (_castCollider.tag == "PushableObject")
            {
                try
                {
                    _sokobanObject = _castResults[0].rigidbody;
                    _sokobanScript.Sokoban(_rb2D, _castResults[0].rigidbody);
                    GameManagerScript.Instance.CurrentMovementMode = MovementMode.Sokoban;
                }
                catch
                {
                    Debug.Log("SokobanInteract failed after finding a PushableObject tag.");
                    return;
                }
            }
        }
        return;
    }

    /// <summary>
    /// PickupInteract calls the Pickup script if interaction range had a pickupable object. Passes the InteractTarget from player script
    /// as an argument and then sets the object inactive.
    /// </summary>
    private void PickupInteract()
    {
        if (GameManagerScript.Instance.CurrentInteractMode == InteractMode.Pickup && GameManagerScript.Instance.CurrentMovementMode == MovementMode.Normal)
        {
            _pickupScript.Pickup(_playerScript.InteractTarget);
            _playerScript.InteractTarget.SetActive(false);
        }
    }

    /// <summary>
    /// TalkInteract checks whether or not the player is currently in dialogue and calls the appropriate method from Dialogue System script
    /// and sets the appropriate mode states and flags.
    /// </summary>
    private void TalkInteract()
    {
        if (GameManagerScript.Instance.IsDialogueActive == false && GameManagerScript.Instance.CurrentMovementMode == MovementMode.Normal)
        {
            UIManagerScript.Instance.DialogueUI.SetActive(true);
            UIManagerScript.Instance.DialogueScript.NewDialogue(_playerScript, _playerScript.InteractTarget);
        }
        else if (GameManagerScript.Instance.IsDialogueActive == true)
        {
            UIManagerScript.Instance.DialogueScript.NextLine();
        }
        return;
    }

    /// <summary>
    /// Originally this was going to handle how the Interact button works in the pause menu, but not enough time to properly implement it.
    /// However it still gets checked, so I'm leaving this stub here as is.
    /// </summary>
    private void PausedInteract()
    {
        //UIManagerScript.Instance.PauseMenu
    }

    /// <summary>
    /// Initialization done in Start so as to run after the singletons just in case. In the future I will not rely on Unity's built in
    /// initialization methods in order to avoid order of execution problems.
    /// I should write more robust error handling but this will do for now.
    /// </summary>
    void Start()
    {
        try
        {
            _rb2D = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        }
        catch
        {
            Debug.Log("ControlParser failed to get Rigidbody2D component from PlayerObject.");
        }
        try
        {
            _sokobanScript = GetComponent<SokobanScript>();
        }
        catch
        {
            Debug.Log("ControlParser failed to get SokobanScript component from PlayerObject.");
        }
        try
        {
            _playerScript = GetComponent<PlayerScript>();
        }
        catch
        {
            Debug.Log("ControlParser failed to get PlayerScript component from PlayerObject.");
        }
        try
        {
            _pickupScript = GetComponent<PlayerPickupScript>();
        }
        catch
        {
            Debug.Log("ControlParser failed to get PlayerPickupScript component from PlayerObject.");
        }
        _moveLeftTimer = 0;
        _moveRightTimer = 0;
        _moveUpTimer = 0;
        _moveDownTimer = 0;
        MoveLeft = false;
        MoveRight = false;
        MoveUp = false;
        MoveDown = false;
        Interact = false;
        Pause = false;
    }
}
