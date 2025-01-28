using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerPickupScript handles adding the picked up item to the player's inventory.
/// </summary>
public class PlayerPickupScript : MonoBehaviour
{
    private PickupableItemScript _pickupScript;
    private PlayerScript _playerScript;

    /// <summary>
    /// Pickup method checks the type and the name of the item from the PickupableItemScript and then sets the appropriate ItemsPickedUp or QuestReward bitmask flag.
    /// </summary>
    /// <param name="_pickupTarget">The GameObject that the Player is trying to pick up.</param>
    public void Pickup(GameObject _pickupTarget)
    {
        try
        {
            _pickupScript = _pickupTarget.GetComponent<PickupableItemScript>();
        }
        catch
        {
            Debug.Log("PlayerPickupScript failed to find PickupableItemScript component from _pickupTarget GameObject.");
            return;
        }
        if (_pickupScript.PickupType == PickupTypes.NPCItem)
        {
            switch (_pickupScript.ItemName)
            {
                case ItemFound.None:
                    _playerScript.PlayerQuestTracker.ItemsPickedUp = _playerScript.PlayerQuestTracker.ItemsPickedUp | PickupableItems.None;
                    break;
                case ItemFound.RichardKey:
                    _playerScript.PlayerQuestTracker.ItemsPickedUp = _playerScript.PlayerQuestTracker.ItemsPickedUp | PickupableItems.RichardKey;
                    break;
                case ItemFound.Item2:
                    _playerScript.PlayerQuestTracker.ItemsPickedUp = _playerScript.PlayerQuestTracker.ItemsPickedUp | PickupableItems.Item2;
                    break;
                case ItemFound.Item3:
                    _playerScript.PlayerQuestTracker.ItemsPickedUp = _playerScript.PlayerQuestTracker.ItemsPickedUp | PickupableItems.Item3;
                    break;
                case ItemFound.Item4:
                    _playerScript.PlayerQuestTracker.ItemsPickedUp = _playerScript.PlayerQuestTracker.ItemsPickedUp | PickupableItems.Item4;
                    break;
                default:
                    Debug.Log("Pickup found nothing to pick up?");
                    break;
            }
        }
        else if (_pickupScript.PickupType == PickupTypes.PotionIngredient)
        {
            switch (_pickupScript.PotionIngredient)
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
        else
        {
            Debug.Log("Somehow PlayerPickupScript messed up.");
        }
    }

    /// <summary>
    /// Initialisation in Start. Simply finds the PlayerScript component from PlayerObject.
    /// </summary>
    private void Start()
    {
        try
        {
            _playerScript = GetComponent<PlayerScript>();
        }
        catch
        {
            Debug.Log("PlayerPickupScript failed to find PlayerScript component from Player object.");
        }
    }
}
