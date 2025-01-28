using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// PauseMenuScript has simple logic for what the two buttons on the pause menu do. There was going to be an extra
/// method here that would check if none of the buttons are currently selected and would set it back to Continue if
/// nothing was selected, but time ran out.
/// </summary>
public class PauseMenuScript : MonoBehaviour
{
    //public GameObject CurrentSelection;

    /// <summary>
    /// LoadMainMenuScene simply loads DemoTitleScene. Just in case it checks that the pause state is active, in case
    /// something goes wrong with EventSystem and it decides to accept background input again.
    /// </summary>
    public void LoadMainMenuScene()
    {
        if (GameManagerScript.Instance.IsGamePaused == true)
        {
            SceneManager.LoadScene("DemoTitleScene");
        }
    }

    /// <summary>
    /// ContinueGame sets the interact and movement modes back to normal and disables the IsGamePaused flag before setting
    /// the pause menu object itself to inactive.
    /// </summary>
    public void ContinueGame()
    {
        GameManagerScript.Instance.IsGamePaused = false;
        GameManagerScript.Instance.CurrentMovementMode = MovementMode.Normal;
        GameManagerScript.Instance.CurrentInteractMode = InteractMode.None;
        UIManagerScript.Instance.PauseMenu.SetActive(false);
    }
}
