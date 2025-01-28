using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerCollisionHandler is used for determining some of the ways the Interact key works using child objects with trigger colliders.
/// _colliderName is a leftover from an earlier implementation that is no longer used but I'm leaving it here in case something blows up.
/// </summary>
public class PlayerCollisionHandler : MonoBehaviour
{
    private PlayerScript _playerScript;
    private string _colliderName;

    /// <summary>
    /// Trigger collision checking is used to see what kind of objects the player is nearby. Mostly we just call TagChecker with the
    /// Collider2D as an argument in order to avoid duplicating code. On Trigger Exit simply sets current interact mode to none.
    /// Current implementation can lead to problems in very specific scenarios, but is fine most of the time.
    /// </summary>
    /// <param name="collision">The Collider2D we 'collided' with.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _colliderName = gameObject.name;
        TagChecker(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        _colliderName = gameObject.name;
        TagChecker(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManagerScript.Instance.CurrentInteractMode = InteractMode.None;
    }

    /// <summary>
    /// TagChecker checks what tag the other collider has and sets the current interact mode appropriately.
    /// In case of pickupable item or an NPC, we set PlayerScript's current interact targe to the collider's parent GameObject.
    /// </summary>
    /// <param name="_collider">The Collider2D component Player object collided with.</param>
    private void TagChecker(Component _collider)
    {
        if (_collider.tag == "PushableObject")
        {
            GameManagerScript.Instance.CurrentInteractMode = InteractMode.Sokoban;
        }
        else if (_collider.tag == "PickupItem")
        {
            GameManagerScript.Instance.CurrentInteractMode = InteractMode.Pickup;
            try
            {
                _playerScript.InteractTarget = _collider.transform.parent.gameObject;
            }
            catch
            {
                Debug.Log("PlayerCollisionHandler failed to find a pickupable object.");
            }
        }
        else if (_collider.tag == "NPC")
        {
            GameManagerScript.Instance.CurrentInteractMode = InteractMode.Talk;
            try
            {
                _playerScript.InteractTarget = _collider.transform.parent.gameObject;
            }
            catch
            {
                Debug.Log("PlayerCollisionHandler failed to find a talkable object.");
            }
        }
        else
        {
            Debug.Log("how did TagChecker get here?"); // Realistically we should never arrive here. This is left here just in case I'm wrong.
            GameManagerScript.Instance.CurrentInteractMode = InteractMode.None;
            _playerScript.InteractTarget = null;
        }
    }

    /// <summary>
    /// Start is used for initialization. CollisionHandler doesn't require much in here, and I could probably have sidestepped needing this entirely.
    /// </summary>
    void Start()
    {
        try
        {
            _playerScript = GetComponentInParent<PlayerScript>();
        }
        catch
        {
            Debug.Log("PlayerCollisionHandler failed to find PlayerScript component from parent Player Object.");
        }
    }
}
