using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script for the player object in the Ball Catcher minigame.
/// </summary>
public class CatcherMovement_BallCatcher : MonoBehaviour
{
    public Rigidbody2D CatcherRb2D;
    [SerializeField] private Vector3 _cameraWorldMin, _cameraWorldMax;
    [SerializeField] private bool b_gameWon, b_gameEnd, b_gameEndConfirm;
    [SerializeField] private float _gameEndTimer;
    private Gyroscope _gyroscope;

    /// <summary>
    /// Initialiser script for the Catcher object. This is called remotely from the main Initialiser script for the Ball Catcher minigame instead of relying on Unity's
    /// built in initialisation methods. It takes a reference to the scene's main camera as a reference so that it can determine movement bounds.
    /// </summary>
    /// <param name="_mainCamera">Camera object for the Ball Catcher minigame.</param>
    public void CatcherInitializer(Camera _mainCamera)
    {
        _gameEndTimer = 0.0f;
        b_gameEnd = false;
        b_gameWon = false;
        b_gameEndConfirm = false;
        CatcherRb2D = GetComponent<Rigidbody2D>();
        try
        {
            _gyroscope = Input.gyro;
            _gyroscope.enabled = true;
            _gyroscope.updateInterval = 0.01f;
        }
        catch (System.Exception)
        {
            Debug.Log("Couldn't initialize gyroscope");
        }
        _cameraWorldMax = _mainCamera.ViewportToWorldPoint(new Vector3(0.95f, 0.95f, 1.0f));
        _cameraWorldMin = _mainCamera.ViewportToWorldPoint(new Vector3(0.05f, 0.05f, 0.3f));
    }

    /// <summary>
    /// Unity's built in OnTriggerEnter2D method. There is a trigger collision box inside the basket graphic.
    /// </summary>
    /// <param name="collision">The collider that touches the trigger collider.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ObjectTypeScript_BallCatcher>().ObjectType == 1)
        {
            b_gameWon = true;
            b_gameEnd = true;
        }
        else
        {
            CatcherRb2D.mass = 0.3f;
            b_gameWon = false;
            b_gameEnd = true;
        }
    }

    /// <summary>
    /// Unity's built in Update method. Using it for checking gyroscope for movement as well as if the minigame has been won/lost.
    /// Yes it's a bad idea to have this in Update. I'm not changing it at this point.
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
                GameManager.Instance.UnloadMinigame(b_gameWon);
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
                    GameManager.Instance.UnloadMinigame(b_gameWon);
                }
                catch (System.Exception)
                {
                    Debug.Log("Couldn't find game manager instance, game is now softlocked.");
                }
            }
        }
        else
        {
            try
            {
                CatcherRb2D.velocity = CatcherRb2D.velocity + new Vector2(Input.acceleration.x, 0.0f);
            }
            catch (System.Exception)
            {
                Debug.Log("Couldn't apply movement properly, is gyroscope working?");
            }
            if (gameObject.transform.position.x >= _cameraWorldMax.x)
            {
                CatcherRb2D.velocity = new Vector2(Mathf.Clamp(CatcherRb2D.velocity.x, -10.0f, 0.0f), 0.0f);
                gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x, _cameraWorldMin.x, _cameraWorldMax.x), gameObject.transform.position.y, gameObject.transform.position.z);
                Debug.Log("catcher x >= camera max x");
            }
            if (gameObject.transform.position.x <= _cameraWorldMin.x)
            {
                CatcherRb2D.velocity = new Vector2(Mathf.Clamp(CatcherRb2D.velocity.x, 0.0f, 10.0f), 0.0f);
                gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x, _cameraWorldMin.x, _cameraWorldMax.x), gameObject.transform.position.y, gameObject.transform.position.z);
                Debug.Log("catcher x <= camera min x");
            }
        }
        // these are left here in case I need them again at some point
        //Debug.Log("Unbiased rotation rate: " + Input.gyro.rotationRateUnbiased + System.Environment.NewLine
        //    + "Biased rotation rate: " + Input.gyro.rotationRate);
        //Debug.Log("Attitude: " + Input.gyro.attitude + System.Environment.NewLine + "Acceleration: " + Input.gyro.userAcceleration);
        //Debug.Log("Attitude: " + Input.gyro.attitude);
    }
}
