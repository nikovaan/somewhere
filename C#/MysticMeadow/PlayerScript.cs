using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main script for player object as well as game's main logic loop. This is the only script that runs any Update or LateUpdate
/// calls in order to enforce order of execution. Only real thing done here besides that is handling player's animations.
/// </summary>
public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D _rb2D;
    private ControlParser _controlParser;
    private Animator _playerAnimator;
    private SpriteRenderer _playerSpriteRenderer;
    private MainCameraScript _cameraScript;
    public bool IsIdle, WalkUp;
    public GameObject InteractTarget;
    public QuestTracker PlayerQuestTracker;

    /// <summary>
    /// Originally these actually did something. I left them here because I don't feel like testing if removing them causes problems.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision entered");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("collision stay");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("collision exit");
    }

    /// <summary>
    /// Handles player's animations. I started a rewrite in order to make it nicer but got sidetracked and time ran out.
    /// </summary>
    public void AnimationState()
    {
        //if (_rb2D.velocity != Vector2.zero)
        //{
        //    if (_rb2D.velocity.y > 0)
        //    {
        //        _playerAnimator.SetBool("Walk", false);
        //        _playerAnimator.SetBool("Walk_Up", true);
        //    }
        //    else if (_rb2D.velocity.y < 0)
        //    {
        //        _playerAnimator.SetBool("Walk_Up", false);
        //        _playerAnimator.SetBool("Walk", true);
        //    }
        //    if (_rb2D.velocity.x > 0)
        //    {
        //        _playerSpriteRenderer.flipX = false;
        //    }
        //    else if (_rb2D.velocity.x < 0)
        //    {
        //        _playerSpriteRenderer.flipX = true;
        //    }
        //}
        //else if (_rb2D.velocity == Vector2.zero)
        //{
        //    // sit still in faced direction?
        //}
        if (_playerAnimator.GetBool("Walk_Up") == true && _rb2D.velocity.y < Mathf.Abs(_rb2D.velocity.x))
        {
            _playerAnimator.SetBool("Walk_Up", false);
        }
        else if (_rb2D.velocity.y > Mathf.Abs(_rb2D.velocity.x))
        {
            _playerAnimator.SetBool("Walk_Up", true);
        }
        else if (_rb2D.velocity != Vector2.zero)
        {
            _playerAnimator.SetBool("Walk", true);
            if (_rb2D.velocity.x > 0)
            {
                _playerSpriteRenderer.flipX = false;
            }
            else if (_rb2D.velocity.x < 0)
            {
                _playerSpriteRenderer.flipX = true;
            }
        }
        else
        {
            _playerAnimator.SetBool("Walk", false);
            _playerAnimator.SetBool("Walk_Up", false);
        }
    }

    /// <summary>
    /// Initialization handled in Start. If the scene the player previously was in was Cave, set player position near the cave entrance.
    /// </summary>
    void Start()
    {
        try
        {
            _rb2D = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        }
        catch
        {
            Debug.Log("PlayerScript failed to Rigidbody2D from PlayerObject.");
        }
        try
        {
            _controlParser = gameObject.GetComponent<ControlParser>();
        }
        catch
        {
            Debug.Log("PlayerScript failed to find ControlParser component from PlayerObject.");
        }
        try
        {
            _playerAnimator = GetComponent<Animator>();
        }
        catch
        {
            Debug.Log("PlayerScript failed to find Animator component from PlayerObject.");
        }
        try
        {
            _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        }
        catch
        {
            Debug.Log("PlayerScript failed to find SpriteRenderer from PlayerObject.");
        }
        try
        {
            _cameraScript = GameManagerScript.Instance.MainCameraObject.GetComponent<MainCameraScript>();
        }
        catch
        {
            Debug.Log("PlayerScript failed to find MainCameraScript component from MainCamera.");
        }
        if (PlayerQuestTracker.PreviousScene == Scenes.Cave)
        {
            GameManagerScript.Instance.PlayerObjectObject.transform.position = new Vector2(10f, 73f);
            PlayerQuestTracker.PreviousScene = Scenes.Forest;
        }
    }

    /// <summary>
    /// Update is called once per frame. Update is used to call control parsing, handling control data, and then applying correct animation states to the player.
    /// </summary>
    void Update()
    {
        _controlParser.InputHandler();
        _controlParser.PlayerControls();
        _controlParser.NormalizeMovement();
        AnimationState();
    }

    /// <summary>
    /// LateUpdate is used for calling camera methods, so that the camera moves only after player's position is finalised for the current frame.
    /// This helps keep camera's movement a bit smoother.
    /// </summary>
    void LateUpdate()
    {
        _cameraScript.CheckPosition();
        _cameraScript.MoveCamera();
    }
}
