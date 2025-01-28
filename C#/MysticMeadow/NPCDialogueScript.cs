using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object for determining which dialogue asset an NPC uses, what item they want (if any), and what item they give (if any).
/// </summary>
public class NPCDialogueScript : MonoBehaviour
{
    public DialogueAsset NPCDialogue;
    [SerializeField] public QuestReward WhichRewardToGive;
    [SerializeField] public PickupableItems ItemWanted;

    /// <summary>
    /// For ease of testing stuff in editor, set NPC quests to revert back to starting point whenever they're loaded. This shouldn't happen in builds,
    /// but it hasn't been tested yet either.
    /// </summary>
    private void Start()
    {
#if UNITY_EDITOR
        NPCDialogue.CurrentQuestProgress = QuestProgress.NotMet;
#endif
    }
}
