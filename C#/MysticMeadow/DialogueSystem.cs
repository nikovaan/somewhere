using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Dialogue system that handles fetching the appropriate dialogue from the dialogue assets and manages the flags for NPCS.
/// It also handles checking if player has found an appropriate item for that the NPC wants, and hands out the appropriate
/// Potion ingredient to the player.
/// </summary>
public class DialogueSystem : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI NameText;
    [SerializeField] public TextMeshProUGUI DialogueText;
    private NPCDialogueScript _dialogueScript;
    private PlayerScript _playerScript;
    private int _dialogueArraySize;
    private int _currentDialogueLine;
    private string[] _currentDialogue;

    /// <summary>
    /// Starts a new dialogue when no dialogue is active. Checks which dialogue asset needs to be grabbed from the
    /// NPC' dialogue asset.
    /// </summary>
    /// <param name="_playerScriptReference">Player GameObject's script component.</param>
    /// <param name="_npcObject">NPC GameObject being talked to.</param>
    public void NewDialogue(PlayerScript _playerScriptReference, GameObject _npcObject)
    {
        _playerScript = _playerScriptReference;
        GameManagerScript.Instance.CurrentMovementMode = MovementMode.None;
        GameManagerScript.Instance.IsDialogueActive = true;
        _dialogueScript = _npcObject.GetComponent<NPCDialogueScript>();
        NameText.text = _dialogueScript.NPCDialogue.NPCName;
        CheckQuestProgress();
        switch (_dialogueScript.NPCDialogue.CurrentQuestProgress)
        {
            case QuestProgress.NotMet:
                _currentDialogue = _dialogueScript.NPCDialogue.NPCDialogue1;
                _dialogueArraySize = _dialogueScript.NPCDialogue.NPCDialogue1.Length;
                _dialogueScript.NPCDialogue.CurrentQuestProgress = QuestProgress.Met;
                break;
            case QuestProgress.Met:
                _currentDialogue = _dialogueScript.NPCDialogue.NPCDialogue2;
                _dialogueArraySize = _dialogueScript.NPCDialogue.NPCDialogue2.Length;
                break;
            case QuestProgress.ObjectiveDone:
                _currentDialogue = _dialogueScript.NPCDialogue.NPCDialogue3;
                _dialogueArraySize = _dialogueScript.NPCDialogue.NPCDialogue3.Length;
                GiveReward();
                break;
            case QuestProgress.RewardReceived:
                _currentDialogue = _dialogueScript.NPCDialogue.NPCDialogue4;
                _dialogueArraySize = _dialogueScript.NPCDialogue.NPCDialogue4.Length;
                break;
            default:
                Debug.Log("NewDialogue somehow didn't pick any of the proper quest states?");
                break;
        }
        DialogueText.text = _currentDialogue[0];
        _currentDialogueLine = 1;
    }

    /// <summary>
    /// Checks if there is any dialogue left in the current conversation, and then either displays the next line of dialogue or
    /// calls EndDialogue to end the conversation.
    /// </summary>
    public void NextLine()
    {
        if (_currentDialogueLine == _dialogueArraySize)
        {
            EndDialogue();
        }
        else
        {
            DialogueText.text = _currentDialogue[_currentDialogueLine];
            _currentDialogueLine = _currentDialogueLine + 1;
        }
    }

    /// <summary>
    /// Ends the current conversation and cleans up some variables and sets things back to normal gameplay.
    /// </summary>
    public void EndDialogue()
    {
        GameManagerScript.Instance.CurrentMovementMode = MovementMode.Normal;
        GameManagerScript.Instance.IsDialogueActive = false;
        _dialogueScript = null;
        _dialogueArraySize = 0;
        _currentDialogueLine = 0;
        UIManagerScript.Instance.DialogueUI.SetActive(false);
    }

    /// <summary>
    /// Gives player the appropriate potion ingredient as a reward for fetching an item for the NPC.
    /// </summary>
    public void GiveReward()
    {
        _dialogueScript.NPCDialogue.CurrentQuestProgress = QuestProgress.RewardReceived;
        switch (_dialogueScript.WhichRewardToGive)
        {
            case QuestReward.None:
                _playerScript.PlayerQuestTracker.CurrentPotionIngredients = _playerScript.PlayerQuestTracker.CurrentPotionIngredients | PotionIngredients.None;
                break;
            case QuestReward.Feather:
                _playerScript.PlayerQuestTracker.CurrentPotionIngredients = _playerScript.PlayerQuestTracker.CurrentPotionIngredients | PotionIngredients.Feather;
                break;
            case QuestReward.SeedPouch:
                _playerScript.PlayerQuestTracker.CurrentPotionIngredients = _playerScript.PlayerQuestTracker.CurrentPotionIngredients | PotionIngredients.SeedPouch;
                break;
            case QuestReward.CrystalWater:
                _playerScript.PlayerQuestTracker.CurrentPotionIngredients = _playerScript.PlayerQuestTracker.CurrentPotionIngredients | PotionIngredients.CrystalWater;
                break;
            case QuestReward.Lingonberry:
                _playerScript.PlayerQuestTracker.CurrentPotionIngredients = _playerScript.PlayerQuestTracker.CurrentPotionIngredients | PotionIngredients.Lingonberry;
                break;
            default:
                Debug.Log("Somehow GiveReward didn't find any reward to give.");
                break;
        }
    }

    /// <summary>
    /// Checks a few flags from the player and the NPC in order to determine current quest progress for said NPC.
    /// </summary>
    public void CheckQuestProgress()
    {
        if (_playerScript.PlayerQuestTracker.ItemsPickedUp.HasFlag(_dialogueScript.ItemWanted) == true && _dialogueScript.NPCDialogue.CurrentQuestProgress != QuestProgress.RewardReceived)
        {
            _dialogueScript.NPCDialogue.CurrentQuestProgress = QuestProgress.ObjectiveDone;
        }
    }
}
