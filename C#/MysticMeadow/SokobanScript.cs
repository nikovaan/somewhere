using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SokobanScript handles latching the player to a movable object and keeping track of distance between player and the movable object.
/// </summary>
public class SokobanScript : MonoBehaviour
{
    private MovableObjectScript _objectScript;
    private float _xDiff, _yDiff, _sokobanDistance;
    private Vector2 _playerPosition, _objectPosition;

    /// <summary>
    /// Sokoban method takes Player object's Rigidbody2D and movable object's Rigidbody2D as arguments. It changes the movable object to
    /// dynamic  rigidbody instead of kinematic in order to side step having to write my own physics handling for colliding kinematic objects.
    /// It also latches the player to the movable object.
    /// </summary>
    /// <param name="_playerRb2D">Player object's Rigidbody2D component.</param>
    /// <param name="_objectRb2D">Movable object's Rigidbody2D component.</param>
    public void Sokoban(Rigidbody2D _playerRb2D, Rigidbody2D _objectRb2D)
    {
        try
        {
            _objectRb2D.useFullKinematicContacts = false;
            _objectRb2D.isKinematic = false;
            _objectScript = _objectRb2D.GetComponent<MovableObjectScript>();
        }
        catch
        {
            Debug.Log("Sokoban script failed to get movable object Rb2D");
            return;
        }
        _xDiff = _playerRb2D.transform.position.x - _objectRb2D.transform.position.x;
        _yDiff = _playerRb2D.transform.position.y - _objectRb2D.transform.position.y;
        if (Mathf.Abs(_xDiff) > Mathf.Abs(_yDiff))
        {
            _playerRb2D.transform.position = new Vector2(_playerRb2D.transform.position.x, _objectRb2D.transform.position.y);
            if (_xDiff > 0)
            {
                _playerRb2D.transform.position = new Vector2(_objectRb2D.transform.position.x + _objectScript.OffsetX, _playerRb2D.transform.position.y);

            }
            else if (_xDiff < 0)
            {
                _playerRb2D.transform.position = new Vector2(_objectRb2D.transform.position.x - _objectScript.OffsetX, _playerRb2D.transform.position.y);
            }
            else
            {
                Debug.Log("something went wrong with looking at xDiff for sokoban");
            }
        }
        else if (Mathf.Abs(_xDiff) < Mathf.Abs(_yDiff))
        {
            _playerRb2D.transform.position = new Vector2(_objectRb2D.transform.position.x, _playerRb2D.transform.position.y);
            if (_yDiff > 0)
            {
                _playerRb2D.transform.position = new Vector2(_playerRb2D.transform.position.x, _objectRb2D.transform.position.y + _objectScript.OffsetY);
            }
            else if (_yDiff < 0)
            {
                _playerRb2D.transform.position = new Vector2(_playerRb2D.transform.position.x, _objectRb2D.transform.position.y - _objectScript.OffsetY);
            }
            else
            {
                Debug.Log("something went wrong with looking at yDiff for sokoban");
            }
        }
        else if (Mathf.Abs(_xDiff) == Mathf.Abs(_yDiff))
        {
            Debug.Log("SokobanScript had the same xDiff and yDiff");
        }
        else
        {
            Debug.Log("comparing xdiff and ydiff for sokoban went wrong somehow");
        }
    }

    /// <summary>
    /// SokobanDistanceChecker keeps track of the distance between player object and movable object.
    /// If the distance becomes greater than 0.05f, it calls EndSokoban.
    /// </summary>
    /// <param name="_playerRb2D">Player object's Rigidbody2D component.</param>
    /// <param name="_objectRb2D">Movable object's Rigidbody2D component.</param>
   public void SokobanDistanceChecker(Rigidbody2D _playerRb2D, Rigidbody2D _objectRb2D)
    {
        _sokobanDistance = 0f;
        _playerPosition = new Vector2(_playerRb2D.transform.position.x, _playerRb2D.transform.position.y);
        _objectPosition = new Vector2(_objectRb2D.transform.position.x, _objectRb2D.transform.position.y);
        _sokobanDistance = Vector2.Distance(_playerPosition, _objectPosition);
        if (_sokobanDistance > 1.05f)
        {
            EndSokoban(_objectRb2D);
        }
        else if (_sokobanDistance < 0.95f)
        {
            EndSokoban(_objectRb2D);
        }
    }

    /// <summary>
    /// EndSokoban sets movement mode back to normal and sets the velocity of the movable object to zero.
    /// It also sets it back to a kinematic rigidbody and sets full kinematic contacts to true.
    /// </summary>
    /// <param name="_objectRb2D">Movable object's Rigidbody2D component.</param>
    public void EndSokoban(Rigidbody2D _objectRb2D)
    {
        GameManagerScript.Instance.CurrentMovementMode = MovementMode.Normal;
        _objectRb2D.velocity = Vector2.zero;
        try
        {
            _objectRb2D.isKinematic = true;
            _objectRb2D.useFullKinematicContacts = true;
        }
        catch
        {
            Debug.Log("EndSokoban script failed change object back to kinematic.");
            return;
        }
    }
}
