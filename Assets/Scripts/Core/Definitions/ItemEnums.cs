/// <summary>
/// Item system enums
/// Consolidated definitions for item rarity and types
/// </summary>

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Lengendary
}

// Note: EquipmentSlot.Slot enum is defined in Item/Item/Equipment.cs as it's specific to Equipment

public enum ItemType
{
    Equipment,
    Consumable,
    Rune,
    Ability,
    Misc
}
