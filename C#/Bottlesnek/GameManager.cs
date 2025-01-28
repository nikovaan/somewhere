using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A game manager singleton because this was 'quick' to make to facilitate holding the game together.
/// Debatable how well it actually works. Should've used events instead I think.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private string _currentMinigames, _sceneName;
    private string[] _minigameSet;
    public bool b_minigameEnded;
    public RuntimeDataScript RuntimeDataAsset;
    private GameObject _pauseRootObject;

    /// <summary>
    /// Call this when starting a run.
    /// </summary>
    public void StartRun(int _startingDifficulty)
    {
        RuntimeDataAsset.HasContinued = false;
        RuntimeDataAsset.MidRun = true;
        RuntimeDataAsset.Difficulty = Mathf.Max(1, _startingDifficulty);
        RuntimeDataAsset.WinCount = 0;
        RuntimeDataAsset.MinigamePointer = 0;
        RuntimeDataAsset.ShuffledMinigames = MinigameShuffler();
        SaveGame();
        SceneManager.LoadSceneAsync("Intermission");
    }

    /// <summary>
    /// Call this when the player ends the run. Checks if the current score is the player's best score, and if their current difficulty is the highest they've gotten to.
    /// Also does some data cleanup before loading the title screen.
    /// </summary>
    public void EndRun()
    {
        if (RuntimeDataAsset.HasContinued == false && RuntimeDataAsset.CurrentScore > RuntimeDataAsset.TopScore)
        {
            RuntimeDataAsset.TopScore = RuntimeDataAsset.CurrentScore;
        }
        else if (RuntimeDataAsset.HasContinued == true)
        {
            RuntimeDataAsset.HasContinued = false;
        }
        if (RuntimeDataAsset.HasContinued == false && RuntimeDataAsset.Difficulty > RuntimeDataAsset.TopDifficulty)
        {
            RuntimeDataAsset.TopDifficulty = RuntimeDataAsset.Difficulty;
        }
        RuntimeDataAsset.MidRun = false;
        RuntimeDataAsset.WinCount = 0;
        RuntimeDataAsset.MinigamePointer = 0;
        SaveGame();
        SceneManager.LoadSceneAsync("TitleScreen");
    }

    /// <summary>
    /// Call this when the player wants to continue their run after clearing the current set of minigames. Adds 1 to the score if the player didn't clear enough minigames last set.
    /// </summary>
    public void Continue()
    {
        if (RuntimeDataAsset.HasContinued == true)
        {
            if (RuntimeDataAsset.CurrentScore % 100 == 0)
            {
                RuntimeDataAsset.CurrentScore = RuntimeDataAsset.CurrentScore + 1;
            }
            else if (RuntimeDataAsset.CurrentScore % 100 < 99)
            {
                RuntimeDataAsset.CurrentScore = RuntimeDataAsset.CurrentScore + 1;
            }
        }
        SaveGame();
        SceneManager.LoadSceneAsync("Intermission");
    }

    /// <summary>
    /// Call this when loading a minigame from the current set. Also checks if the current minigame set was finished.
    /// </summary>
    public void LoadMinigame()
    {
        b_minigameEnded = false;
        if (RuntimeDataAsset.MinigamePointer > 4)
        {
            Debug.Log("Set finished");
            SetFinished();
        }
        else
        {
            SceneManager.LoadSceneAsync(RuntimeDataAsset.ShuffledMinigames[RuntimeDataAsset.MinigamePointer]);
            //SceneManager.LoadSceneAsync("BallCatcher");
        }
    }

    /// <summary>
    /// Switch active scene back to the mid-run scene, async unload the previous minigame, load the current state of the run.
    /// </summary>
    /// <param name="b_gameWon">Boolean for whether the minigame was won or lost.</param>
    public void UnloadMinigame(bool b_gameWon)
    {
        if (b_minigameEnded == false)
        {
            b_minigameEnded = true;
            if (b_gameWon == true)
            {
                RuntimeDataAsset.WinCount = Mathf.Min(RuntimeDataAsset.WinCount + 1, 5);
                RuntimeDataAsset.CurrentScore = RuntimeDataAsset.CurrentScore + (100 + (((RuntimeDataAsset.Difficulty * RuntimeDataAsset.Difficulty) - 1) * 100) + ((RuntimeDataAsset.WinCount - 1) * 200));
            }
            RuntimeDataAsset.MinigamePointer = RuntimeDataAsset.MinigamePointer + 1;
            SaveGame();
            if (RuntimeDataAsset.MinigamePointer > 4)
            {
                SetFinished();
            }
            else
            {
                SceneManager.LoadSceneAsync("Intermission", LoadSceneMode.Single);
            }
        }
    }

    /// <summary>
    /// Call this when the current set of minigames is finished. Tallies results, gets the next set.
    /// </summary>
    public void SetFinished()
    {
        if (RuntimeDataAsset.WinCount >= Mathf.Min(RuntimeDataAsset.Difficulty, 5))
        {
            RuntimeDataAsset.Difficulty = Mathf.Max(1, RuntimeDataAsset.Difficulty + 1);
            if (RuntimeDataAsset.HasContinued == false && RuntimeDataAsset.CurrentScore > RuntimeDataAsset.TopScore)
            {
                RuntimeDataAsset.TopScore = RuntimeDataAsset.CurrentScore;
            }
            if (RuntimeDataAsset.HasContinued == false && RuntimeDataAsset.Difficulty > RuntimeDataAsset.TopDifficulty)
            {
                RuntimeDataAsset.TopDifficulty = RuntimeDataAsset.Difficulty;
            }
            RuntimeDataAsset.WinCount = 0;
            RuntimeDataAsset.MinigamePointer = 0;
            RuntimeDataAsset.ShuffledMinigames = MinigameShuffler();
            SceneManager.LoadSceneAsync("ScoreScreen");
        }
        else
        {
            RuntimeDataAsset.HasContinued = true;
            RuntimeDataAsset.WinCount = 0;
            RuntimeDataAsset.MinigamePointer = 0;
            RuntimeDataAsset.ShuffledMinigames = MinigameShuffler();
            SceneManager.LoadSceneAsync("ScoreScreen");
        }
    }

    /// <summary>
    /// Call this when pausing the game. Stores a reference to the root object of the paused minigame, additively loads the PauseMenu
    /// scene and deactivates the minigame root object.
    /// </summary>
    /// <param name="_rootObject">The root GameObject for a minigame scene.</param>
    public void PauseGame(GameObject _rootObject)
    {
        _pauseRootObject = _rootObject;
        SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("PauseMenu"));
        _rootObject.SetActive(false);
    }

    /// <summary>
    /// Call this when unpausing the game. Switches the active scene to the minigame scene, unloads PauseMenu scene and activates the
    /// minigame root object.
    /// </summary>
    public void UnPauseGame()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(RuntimeDataAsset.ShuffledMinigames[RuntimeDataAsset.MinigamePointer]));
        SceneManager.UnloadSceneAsync("PauseMenu");
        _pauseRootObject.SetActive(true);
    }

    /// <summary>
    /// Call this when data needs to be saved. Saves whatever is important to PlayerPrefs.
    /// </summary>
    public void SaveGame()
    {
        PlayerPrefs.SetInt("TopScore", RuntimeDataAsset.TopScore);
        PlayerPrefs.SetInt("TopDifficulty", RuntimeDataAsset.TopDifficulty);
        if (RuntimeDataAsset.MidRun == true)
        {
            PlayerPrefs.SetInt("RunInterrupted", 1);
            PlayerPrefs.SetInt("CurrentDifficulty", RuntimeDataAsset.Difficulty);
            PlayerPrefs.SetInt("CurrentScore", RuntimeDataAsset.CurrentScore);
            PlayerPrefs.SetInt("CurrentWinCount", RuntimeDataAsset.WinCount);
            PlayerPrefs.SetInt("SetProgress", RuntimeDataAsset.MinigamePointer);
            if (RuntimeDataAsset.HasContinued == true)
            {
                PlayerPrefs.SetInt("HasContinued", 1);
            }
            else
            {
                PlayerPrefs.SetInt("HasContinued", 0);
            }
            _currentMinigames = String.Join(",", RuntimeDataAsset.ShuffledMinigames);
            PlayerPrefs.SetString("CurrentMinigameSet", _currentMinigames);
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Call this method when you want to load the player's save from PlayerPrefs. Currently only loads top score and difficulty.
    /// </summary>
    /// <returns>Returns a string for either the Intermission or TitleScreen scene depending on if the game was saved mid-run.</returns>
    public string LoadGame()
    {
        RuntimeDataAsset.TopScore = PlayerPrefs.GetInt("TopScore", 0);
        RuntimeDataAsset.TopDifficulty = PlayerPrefs.GetInt("TopDifficulty", 1);
        /*if (PlayerPrefs.GetInt("RunInterrupted", 0) == 1)
        {
            RuntimeDataAsset.Difficulty = PlayerPrefs.GetInt("CurrentDifficulty", 1);
            RuntimeDataAsset.CurrentScore = PlayerPrefs.GetInt("CurrentScore", 0);
            RuntimeDataAsset.WinCount = PlayerPrefs.GetInt("CurrentWinCount", 0);
            RuntimeDataAsset.MinigamePointer = PlayerPrefs.GetInt("SetProgress", 0);
            if (PlayerPrefs.GetInt("HasContinued", 0) == 1)
            {
                RuntimeDataAsset.HasContinued = true;
            }
            else
            {
                RuntimeDataAsset.HasContinued = false;
            }
            if (PlayerPrefs.HasKey("CurrentMinigameSet") == true)
            {
                try
                {
                    _minigameSet = PlayerPrefs.GetString("CurrentMinigameSet").Split(",");
                    RuntimeDataAsset.ShuffledMinigames = _minigameSet;
                    _sceneName = "Intermission";
                }
                catch (System.Exception)
                {
                    Debug.Log("Minigames were not saved properly.");
                    _sceneName =  "TitleScreen";
                }
            }
            else
            {
                _sceneName = "TitleScreen";
            }
        }
        else
        {
            _sceneName = "TitleScreen";
        }*/
        _sceneName = "TitleScreen";
        return _sceneName;
    }

    /// <summary>
    /// Used to access the current difficulty as integer from the RuntimeDataAsset scriptable object.
    /// </summary>
    /// <returns>Returns the current difficulty as integer.</returns>
    public int Difficulty()
    {
        return Mathf.Max(RuntimeDataAsset.Difficulty, 1);
    }

    /// <summary>
    /// Used to access the player's highest difficulty as integer from the RuntimeDataAsset scriptable object.
    /// </summary>
    /// <returns>Returns the player's highest difficulty as integer.</returns>
    public int TopDifficulty()
    {
        return RuntimeDataAsset.TopDifficulty;
    }

    /// <summary>
    /// Used to access the current score as integer from the RuntimeDataAsset scriptable object.
    /// </summary>
    /// <returns>Returns the current score as integer.</returns>
    public int CurrentScore()
    {
        return RuntimeDataAsset.CurrentScore;
    }

    /// <summary>
    /// Used to access the player's top score as integer from the RuntimeDataAsset scriptable object.
    /// </summary>
    /// <returns>Returns the player's top score as integer.</returns>
    public int TopScore()
    {
        return RuntimeDataAsset.TopScore;
    }

    /// <summary>
    /// Used to access the current win count as integer from the RuntimeDataAsset scriptable object.
    /// </summary>
    /// <returns>Returns the current win count as integer.</returns>
    public int WinCount()
    {
        return RuntimeDataAsset.WinCount;
    }

    /// <summary>
    /// Used to access the current minigame progression as integer from the RuntimeDataAsset scriptable object.
    /// </summary>
    /// <returns>Returns an integer that indicates how far in the minigame set the player is.</returns>
    public int MinigamePointer()
    {
        return RuntimeDataAsset.MinigamePointer;
    }

    /// <summary>
    /// Used to check if the player has used a continue during the run from the RuntimeDataAsset scriptable object.
    /// </summary>
    /// <returns>Returns a boolean that tells if the player has used a continue or not.</returns>
    public bool HasContinued()
    {
        return RuntimeDataAsset.HasContinued;
    }

    /// <summary>
    /// Used to check if the run is currently ongoing from the RuntimeDataAsset scriptable object.
    /// </summary>
    /// <returns>Returns a boolean that tells if the game was interrupted or not.</returns>
    public bool MidRun()
    {
        return RuntimeDataAsset.MidRun;
    }

    /// <summary>
    /// Used to access the current shuffled minigames list from the RuntimeDataAsset scriptable object.
    /// </summary>
    /// <returns>Returns the shuffled minigames list as a string array.</returns>
    public string[] ShuffledMinigames()
    {
        return RuntimeDataAsset.ShuffledMinigames;
    }

    /// <summary>
    /// Used to access the main camera object from the RuntimeDataAsset scriptable object.
    /// </summary>
    /// <returns>Returns the Camera object for the main camera.</returns>
    public Camera MainCameraObject()
    {
        return RuntimeDataAsset.MainCameraObject;
    }

    /// <summary>
    /// Used to access the main camera CamAdjust script from the RuntimeDataAsset scriptable object.
    /// </summary>
    /// <returns>Returns the CamAdjust script for the main camera.</returns>
    public CamAdjust MainCameraScript()
    {
        return RuntimeDataAsset.MainCameraScript;
    }

    /// <summary>
    /// Used to set a reference to the main camera object. This is only called at game launch.
    /// </summary>
    /// <param name="_mainCameraObject">A reference to a Camera object.</param>
    public void SetMainCameraObject(Camera _mainCameraObject)
    {
        RuntimeDataAsset.MainCameraObject = _mainCameraObject;
    }

    /// <summary>
    /// Used to set a reference to the main camera script. This is only called at game launch.
    /// </summary>
    /// <param name="_mainCameraScript">A reference to the CamAdjust script.</param>
    public void SetMainCameraScript(CamAdjust _mainCameraScript)
    {
        RuntimeDataAsset.MainCameraScript = _mainCameraScript;
    }

    /// <summary>
    /// Passes the background image to the main camera.
    /// </summary>
    /// <param name="_backgroundObject">Gameobject that contains a sprite renderer component that has the background image for the minigame.</param>
    private void SetCameraBackground(GameObject _backgroundObject)
    {
        try
        {
            RuntimeDataAsset.MainCameraScript.bgImage = _backgroundObject;
        }
        catch (Exception)
        {
            Debug.Log("SetCameraBackround failed.");
        }
    }

    /// <summary>
    /// Go into portrait mode. Currently unused.
    /// </summary>
    public void PortraitMode()
    {
        if (Screen.orientation != ScreenOrientation.Portrait)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    /// <summary>
    /// Go into landscape mode. Currently unused.
    /// </summary>
    public void LandscapeMode()
    {
        if (Screen.orientation != ScreenOrientation.LandscapeLeft)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }

    /// <summary>
    /// Checks that there aren't other game managers currently in existence.
    /// </summary>
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        b_minigameEnded = false;
    }

    /// <summary>
    /// Checks that there aren't other game managers currently in existence.
    /// </summary>
    public void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        b_minigameEnded = false;
    }

    /// <summary>
    /// Knuth shuffle algorithm for randomizing the order of minigames.
    /// </summary>
    /// <returns>String array of the minigame scene names in a shuffled order.</returns>
    private string[] MinigameShuffler()
    {
        string[] _minigameScenes = Enum.GetNames(typeof(MinigameScenes));
        for (int i = 0; i < _minigameScenes.Length; i++)
        {
            string _shufflerTemp = _minigameScenes[i];
            int _shuffledPointer = UnityEngine.Random.Range(i, _minigameScenes.Length);
            _minigameScenes[i] = _minigameScenes[_shuffledPointer];
            _minigameScenes[_shuffledPointer] = _shufflerTemp;
        }
        return _minigameScenes;
    }
}

/// <summary>
/// All the current minigame scene names. Primarily used for shuffling the order for minigames.
/// </summary>
public enum MinigameScenes
{
    BallCatcher,
    BleachBaby,
    ButterSpread,
    CameraZoom,
    CardScene,
    DragBabyShapes,
    EggHatch,
    PetScene,
    PourTea,
    ShakeScene
}