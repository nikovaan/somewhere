using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// QuestTracker is a scriptable object used for tracking what items the player has picked up and what potion ingredients
/// the player has obtained so far. I added it to the create asset menu just for ease of use.
/// </summary>
[CreateAssetMenu]
public class QuestTracker : ScriptableObject
{
    [SerializeField] public PotionIngredients CurrentPotionIngredients;
    [SerializeField] public PickupableItems ItemsPickedUp;
    [SerializeField] public Scenes PreviousScene;
}
