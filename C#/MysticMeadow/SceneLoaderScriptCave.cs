using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SceneLoaderScriptCave checks if an object with Player tag collided with it and then loads CaveScene. It also passes which scene
/// we're leaving to the player quest tracker. This is entirely because a different implementation would be +1 script file and
/// I can't be bothered at this point.
/// </summary>
public class SceneLoaderScriptCave : MonoBehaviour
{
    [SerializeField] private QuestTracker _playerQuestTracker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerQuestTracker.PreviousScene = Scenes.Forest;
            SceneManager.LoadScene("CaveScene");
        }
    }
}
