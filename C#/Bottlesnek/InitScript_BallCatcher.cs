using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// This is the initializer script for the Ball Catcher minigame.
/// </summary>
public class Init_BallCatcher : MonoBehaviour
{
    [SerializeField] private int _difficulty = 1;
    public GameObject CatcherObject;
    public CatcherMovement_BallCatcher CatcherScript;
    [SerializeField] public GameObject TrashObject;
    [SerializeField] public GameObject BallObject;
    [SerializeField] public GameObject BackgroundObject;
    public ObjectTypeScript_BallCatcher ObjectScript;
    public Camera CameraObject;
    private Vector3 _spawnPointPosition;
    private int _spawnPointCount, _spawnWaves;
    private Vector3[] _spawnPoints;
    private float _screenWidthInterval;
    private WaitForSeconds _waveDelay = new WaitForSeconds(0.4f);
    private WaitForSeconds _spawnDelay = new WaitForSeconds(0.2f);
    [SerializeField] private GameObject _rootObject;

    /// <summary>
    /// Get references to all the objects in the scene and their scripts, and call the initializer method for each script.
    /// </summary>
    void Start()
    {
        try
        {
            _difficulty = Mathf.Max(1, GameManager.Instance.Difficulty());
            
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to get difficulty from game manager");
            _difficulty = 1;
        }
        try
        {
            CatcherObject = GameObject.Find("CatcherPrefab_BallCatcher");
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to find the catcher object");
        }
        try
        {
            CameraObject = Camera.main;
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to find the main camera");
        }
        try
        {
            CatcherScript = CatcherObject.GetComponent<CatcherMovement_BallCatcher>();
            CatcherScript.CatcherInitializer(CameraObject);
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to initialize catcher script");
        }
        try
        {
            _rootObject = GameObject.FindWithTag("RootObject");
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to find root object in start of initscript");
        }
        try
        {
            ObjectGenerator();
        }
        catch (System.Exception)
        {
            Debug.Log("ObjectGenerator failed");
        }
    }


    /// <summary>
    /// Generates a quantity of trash objects based on current difficulty level as well as the ball object.
    /// Also places the objects randomly without many sanity checks.
    /// </summary>
    private void ObjectGenerator()
    {
        _spawnPointCount = (Screen.width / 100) - 2;
        _spawnPoints = new Vector3[_spawnPointCount];
        _screenWidthInterval = 1.0f / _spawnPointCount;

        for (int i = 0; i < _spawnPointCount; i++)
        {
            _spawnPointPosition = CameraObject.ViewportToWorldPoint(new Vector3(_screenWidthInterval * (i + 1), 0.9f, -1.0f));
            _spawnPoints[i] = new Vector3(_spawnPointPosition.x, _spawnPointPosition.y, -1.0f);
        }

        if (_difficulty * 2 + 1 >= _spawnPointCount)
        {
            _spawnWaves = Mathf.FloorToInt((_difficulty * 2 + 1) / _spawnPointCount);
            StartCoroutine(StaggeredSpawner(_spawnWaves));
        }
        else
        {
            _spawnPoints = Shuffler(_spawnPoints);

            for (int j = 0; j < _difficulty * 2 + 1; j++)
            {
                if (j < (_difficulty * 2))
                {
                    Instantiate(TrashObject, _spawnPoints[j], Quaternion.identity, _rootObject.transform);
                }
                else
                {
                    Instantiate(BallObject, _spawnPoints[j], Quaternion.identity, _rootObject.transform);
                }
            }
        }
    }

    /// <summary>
    /// Knuth shuffle algorithm for randomizing which spawn point gets used
    /// </summary>
    private Vector3[] Shuffler(Vector3[] _spawnPointList)
    {
        for (int k = 0; k < _spawnPointList.Length - 1; k++)
        {
            Vector3 _shufflerTemp = _spawnPointList[k];
            int _shuffledPointer = Random.Range(k, _spawnPointList.Length - 1);
            _spawnPointList[k] = _spawnPointList[_shuffledPointer];
            _spawnPointList[_shuffledPointer] = _shufflerTemp;
        }
        return _spawnPointList;
    }

    /// <summary>
    /// A coroutine for spawning trash in waves before finally spawning the ball you need to catch when difficulty is high enough.
    /// </summary>
    /// <param name="_waves">Integer for many waves of trash will be spawned.</param>
    /// <returns></returns>
    private IEnumerator StaggeredSpawner(int _waves)
    {
        int i = 0;
        while (i <= _waves)
        {
            _spawnPoints = Shuffler(_spawnPoints);
            if (i < _waves)
            {
                for (int j = 0;  j < _spawnPointCount; j++)
                {
                    Instantiate(TrashObject, _spawnPoints[j], Quaternion.identity, _rootObject.transform);
                    yield return _spawnDelay;
                }
            }
            
            else if (i == _waves)
            {
                for (int j = 0; j < _spawnPointCount; j++)
                {
                    if (j < _difficulty * 2)
                    {
                        Instantiate(TrashObject, _spawnPoints[j], Quaternion.identity, _rootObject.transform);
                        yield return _spawnDelay;
                    }
                }

                Instantiate(BallObject, _spawnPoints[i], Quaternion.identity);
                yield break;
            }

            else
            {
                Debug.Log("StaggeredSpawner coroutine somehow spawned too many waves?");
            }
            
            i++;
            yield return _waveDelay;
        }
    }
}