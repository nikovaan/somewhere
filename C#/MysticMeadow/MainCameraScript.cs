using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code for main camera. It checks player's position within the viewport and then moves the camera depending on how far the player is from the center.
/// The various Serialized floats are used in editor to define camera movement bounds. This means every map must be a rectangle.
/// </summary>
public class MainCameraScript : MonoBehaviour
{
    private int _moveTimer;
    private float _xMin, _xMax, _xDiff, _yMin, _yMax, _yDiff, _xDeadZoneMax, _xDeadZoneMin, _yDeadZoneMax, _yDeadZoneMin;
    [SerializeField] private float _xMaxWorld, _xMinWorld, _yMaxWorld, _yMinWorld;
    private Vector3 _viewportPosition, _viewportCenter, _cameraWorldMax, _cameraWorldMin, _cameraWorldCenter, _playerWorldPosition, _cameraMove, _cameraBoundsCheck;
    private bool _yBounds, _yBoundsLower, _yBoundsHigher, _xBounds, _xBoundsLower, _xBoundsHigher;
    private Rigidbody2D _playerRb2D;

    /// <summary>
    /// MoveCamera moves the camera dynamically based on player's position and level bounds as defined in editor.
    /// </summary>
    public void MoveCamera()
    {
        _cameraWorldMax = GameManagerScript.Instance.MainCameraObject.ViewportToWorldPoint(new Vector3(0.8f, 0.8f, 10.00f));
        _cameraWorldMin = GameManagerScript.Instance.MainCameraObject.ViewportToWorldPoint(new Vector3(0.2f, 0.2f, 10.00f));
        _cameraWorldCenter = GameManagerScript.Instance.MainCameraObject.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10.00f));
        _playerWorldPosition = GameManagerScript.Instance.PlayerObjectObject.transform.position;
        if (_xBounds == false)
        {
            if (_xBoundsHigher == true)
            {
                _xDiff = _cameraWorldMax.x - _playerWorldPosition.x;
                if (_xDiff == 0)
                {
                    _xDiff = -0.1f;
                }
            }

            else if (_xBoundsLower == true)
            {
                _xDiff = _cameraWorldMin.x - _playerWorldPosition.x;
                if (_xDiff == 0)
                {
                    _xDiff = 0.1f;
                }
            }
        }
        else if (_xBounds == true && _yBounds == true && _moveTimer >= 30)
        {
            _xDiff = _cameraWorldCenter.x - _playerWorldPosition.x;
            _xDiff = Mathf.Clamp(_xDiff, -0.1f, 0.1f);
        }

        if (_yBounds == false)
        {
            if (_yBoundsHigher == true)
            {
                _yDiff = _cameraWorldMax.y - _playerWorldPosition.y;
                if (_yDiff == 0)
                {
                    _yDiff = -0.1f;
                }
            }
            else if (_yBoundsLower == true)
            {
                _yDiff = _cameraWorldMin.y - _playerWorldPosition.y;
                if (_yDiff == 0)
                {
                    _yDiff = 0.1f;
                }
            }
        }
        else if (_yBounds == true && _xBounds == true && _moveTimer >= 30)
        {
            _yDiff = _cameraWorldCenter.y - _playerWorldPosition.y;
            _yDiff = Mathf.Clamp(_yDiff, -0.1f, 0.1f);
        }

        _cameraMove = new Vector3(_xDiff, _yDiff, 0f);
        _xDiff = 0f;
        _yDiff = 0f;
        if (_cameraMove != Vector3.zero)
        {
            _cameraBoundsCheck = new Vector3(GameManagerScript.Instance.MainCameraObject.transform.position.x,
                GameManagerScript.Instance.MainCameraObject.transform.position.y,
                GameManagerScript.Instance.MainCameraObject.transform.position.z);
            _cameraBoundsCheck = _cameraBoundsCheck - _cameraMove;
            if (_cameraBoundsCheck.x <= _xMinWorld)
            {
                _cameraMove.x = 0f;
            }
            if (_cameraBoundsCheck.x >= _xMaxWorld)
            {
                _cameraMove.x = 0f;
            }
            if (_cameraBoundsCheck.y <= _yMinWorld)
            {
                _cameraMove.y = 0f;
            }
            if (_cameraBoundsCheck.y >= _yMaxWorld)
            {
                _cameraMove.y = 0f;
            }
            _cameraBoundsCheck = Vector3.zero;
            GameManagerScript.Instance.MainCameraObject.transform.position = GameManagerScript.Instance.MainCameraObject.transform.position - _cameraMove;
            _cameraMove = Vector3.zero;
        }
    }

    /// <summary>
    /// CheckPosition checks player's position within the viewport, and sets camera movement flags and timers accordingly.
    /// </summary>
    public void CheckPosition()
    {
        _viewportPosition = GameManagerScript.Instance.MainCameraObject.WorldToViewportPoint(GameManagerScript.Instance.PlayerObjectObject.transform.position);
        if (_viewportPosition.x > _xMax)
        {
            _xBounds = false;
            _xBoundsHigher = true;
            _xBoundsLower = false;
            _moveTimer = 30;
        }
        else if (_viewportPosition.x < _xMin)
        {
            _xBounds = false;
            _xBoundsLower = true;
            _xBoundsHigher = false;
            _moveTimer = 30;
        }
        else
        {
            _xBounds = true;
            _xBoundsLower = false;
            _xBoundsHigher = false;
        }
        if (_viewportPosition.y > _yMax)
        {
            _yBounds = false;
            _yBoundsHigher = true;
            _yBoundsLower = false;
            _moveTimer = 30;
        }
        else if (_viewportPosition.y < _yMin)
        {
            _yBounds = false;
            _yBoundsLower = true;
            _yBoundsHigher = false;
            _moveTimer = 30;
        }
        else
        {
            _yBounds = true;
            _yBoundsLower = false;
            _yBoundsHigher = false;
        }
        if (_yBounds == true && _xBounds == true)
        {
            if (_viewportPosition == _viewportCenter && _playerRb2D.velocity == Vector2.zero)
            {
                _moveTimer = 0;
                return;
            }
            if (_viewportPosition.x < _xDeadZoneMin)
            {
                _moveTimer = _moveTimer + 1;
            }
            else if (_viewportPosition.x > _xDeadZoneMax)
            {
                _moveTimer = _moveTimer + 1;
            }
            if (_viewportPosition.y < _yDeadZoneMin)
            {
                _moveTimer = _moveTimer + 1;
            }
            else if (_viewportPosition.y > _yDeadZoneMax)
            {
                _moveTimer = _moveTimer + 1;
            }
        }
    }

    /// <summary>
    /// Initialization is done in Start. Ideally we should simply avoid Unity's built in initialization methods as their execution order is not set,
    /// but it will do for this project. The various max/minworld values are now set per scene on editor, but I left the old values here anyway.
    /// </summary>
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        _viewportCenter = new Vector3(0.50f, 0.50f, 10.00f);
        try
        {
            _playerRb2D = GameManagerScript.Instance.PlayerObjectObject.GetComponent<Rigidbody2D>();
        }
        catch
        {
            Debug.Log("MainCamera failed to get player rigidbody in Start for some reason");
        }
        //_xMaxWorld = 35;
        //_xMinWorld = -11;
        //_yMaxWorld = 71;
        //_yMinWorld = 5;
        _moveTimer = 0;
        _xMin = 0.20f;
        _xMax = 0.80f;
        _yMin = 0.20f;
        _yMax = 0.80f;
        _xDeadZoneMin = 0.45f;
        _xDeadZoneMax = 0.55f;
        _yDeadZoneMin = 0.45f;
        _yDeadZoneMax = 0.55f;
    }
}
