using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This file contains a collection of commonly used enums that are checked in several scripts.
/// </summary>

public enum MovementMode
{
    Normal,
    Sokoban,
    None
}

public enum InteractMode
{
    None,
    Sokoban,
    Pickup,
    Talk,
    Pause
}
public enum QuestProgress
{
    NotMet,
    Met,
    ObjectiveDone,
    RewardReceived
}

public enum PickupTypes
{
    NPCItem,
    PotionIngredient
}

[Flags]
public enum PotionIngredients
{
    None = 0b_0000,
    Feather = 0b_0001,
    SeedPouch = 0b_0010,
    CrystalWater = 0b_0100,
    Lingonberry = 0b_1000
}

public enum QuestReward
{
    None,
    Feather,
    SeedPouch,
    CrystalWater,
    Lingonberry
}

[Flags]
public enum PickupableItems
{
    None = 0b_0000_0000,
    RichardKey = 0b_0000_0001,
    Item2 = 0b_0000_0010,
    Item3 = 0b_0000_0100,
    Item4 = 0b_0000_1000,
    NoItemWanted = 0b_0001_0000
}

public enum ItemFound
{
    None,
    RichardKey,
    Item2,
    Item3,
    Item4
}

public enum Scenes
{
    Title,
    Forest,
    Cave
}