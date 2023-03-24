using UnityEditor;
using UnityEngine;

public class ItemWood : Item
{
    public ItemWood(int stackAmount)
    {
        id = 2;
        itemName = "Wood";
        Stacks = stackAmount;
        maxStacks = 1;
        sprite = SpriteLibary.GetSprite(id);
    }
}