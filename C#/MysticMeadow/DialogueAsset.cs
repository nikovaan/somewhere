using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DialogueAsset is a scriptable object that allows you to set the quest progress for an NPC and what name
/// the dialogue box shows and four different sets of dialogue based on current quest state.
/// </summary>
[CreateAssetMenu]
public class DialogueAsset : ScriptableObject
{
    [SerializeField] public QuestProgress CurrentQuestProgress;
    [TextArea] public string NPCName;
    [TextArea] public string[] NPCDialogue1;
    [TextArea] public string[] NPCDialogue2;
    [TextArea] public string[] NPCDialogue3;
    [TextArea] public string[] NPCDialogue4;
}
