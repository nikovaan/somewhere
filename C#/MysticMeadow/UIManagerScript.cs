using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI Manager is a public singleton that gets references to the pause and dialogue objects and then disables them.
/// This is done for ease of accessing said objects via other scripts later on.
/// </summary>
public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript Instance { get; private set; }
    public GameObject PauseMenu;
    public GameObject DialogueUI;
    public DialogueSystem DialogueScript;

    /// <summary>
    /// Initialization done in Awake so that it runs before the initialization of most other objects. In the future I will not rely
    /// on Unity's built in initialization methods due to order of execution problems.
    /// I should set up more robust error handling for the initialization but for now this will do.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("For some reason there was a duplicate UI Manager?");
            Destroy(this);
        }
        try
        {
            PauseMenu = GameObject.Find("PauseCanvas");
            PauseMenu.SetActive(false);
        }
        catch
        {
            Debug.Log("UIManagerScript failed to find PauseCanvas");
        }
        try
        {
            DialogueUI = GameObject.Find("DialogueCanvas");
            DialogueScript = DialogueUI.GetComponent<DialogueSystem>();
            DialogueUI.SetActive(false);
        }
        catch
        {
            Debug.Log("UIManagerScript failed to find DialogueCanvas");
        }
    }
}
