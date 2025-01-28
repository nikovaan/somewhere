using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A scriptable object for accessing the play instructions for each minigame for Intermission purposes.
/// </summary>
[CreateAssetMenu]
public class MinigameInstructionsScript : ScriptableObject
{
    [TextArea] public string BallCatcher;
    [TextArea] public string BleachBaby;
    [TextArea] public string ButterSpread;
    [TextArea] public string CameraZoom;
    [TextArea] public string CardScene;
    [TextArea] public string DragBabyShapes;
    [TextArea] public string EggHatch;
    [TextArea] public string PetScene;
    [TextArea] public string PourTea;
    [TextArea] public string ShakeScene;

    /// <summary>
    /// Call this method when you want to get the instructions for a minigame as a string.
    /// </summary>
    /// <param name="_minigameName">String that contains the name of the minigame instructions variable you want instructions for.</param>
    /// <returns>Returns a string that has the instructions for a given minigame.</returns>
    public string GetInstructions(string _minigameName)
    {
        switch (_minigameName)
        {
            case "BallCatcher": return BallCatcher;
            case "BleachBaby": return BleachBaby;
            case "ButterSpread": return ButterSpread;
            case "CameraZoom": return CameraZoom;
            case "CardScene": return CardScene;
            case "DragBabyShapes": return DragBabyShapes;
            case "EggHatch": return EggHatch;
            case "PetScene": return PetScene;
            case "PourTea": return PourTea;
            case "ShakeScene": return ShakeScene;
            default:
                break;
        }
        return "No instructions found";
    }
}