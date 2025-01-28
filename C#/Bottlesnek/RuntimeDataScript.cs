using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A scriptable object that stores miscellaneous runtime data that's required in a lot of places.
/// </summary>
[CreateAssetMenu]
public class RuntimeDataScript : ScriptableObject
{
    public int Difficulty, TopDifficulty, CurrentScore, TopScore, WinCount;
    public bool HasContinued, MidRun;
    public string[] ShuffledMinigames;
    public int MinigamePointer;
    public Camera MainCameraObject;
    public CamAdjust MainCameraScript;
}
