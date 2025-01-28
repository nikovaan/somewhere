using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// A minor script that's attached to pickupable items that NPCs want that lets you select which item it is using the
/// ItemFound enum.
/// </summary>
public class PickupableItemScript : MonoBehaviour
{
    [SerializeField] public PickupTypes PickupType;
    [SerializeField] public ItemFound ItemName;
    [SerializeField] public QuestReward PotionIngredient;
}
