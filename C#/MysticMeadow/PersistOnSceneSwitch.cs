using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A small script that will be attached to objects that need to not be destroyed on scene load. So far that's only the audio player.
/// </summary>
public class PersistOnSceneSwitch : MonoBehaviour
{
    private GameObject[] _gameObjects;
    /// <summary>
    /// Since this objects persists between loads, there should never be a second one and we shouldn't need to make a new one.
    /// Currently it only makes sure there aren't duplicates with the tag MusicPlayer. Ideally this should be more robust,
    /// but as this is currently only for the audio player it's fine.
    /// </summary>
    void Awake()
    {
        try
        {
            _gameObjects = GameObject.FindGameObjectsWithTag("MusicPlayer");
        }
        catch
        {
            Debug.Log("PersistOnSceneSwitch failed to find any objects with MusicPlayer tag.");
        }

        if (_gameObjects.Length > 1 )
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
