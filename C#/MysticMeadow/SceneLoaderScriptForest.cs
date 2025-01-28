using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SceneLoaderScriptForest checks if an object with the Player tag collides with it and then loads DemoGameScene. It also sets what scene
/// the player is leaving to player quest tracker AKA sets it to Cave. This is so that I don't have to make +1 script file.
/// </summary>
public class SceneLoaderScriptForest : MonoBehaviour
{
    [SerializeField] private QuestTracker _playerQuestTracker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerQuestTracker.PreviousScene = Scenes.Cave;
            SceneManager.LoadScene("DemoGameScene");
        }
    }
}
