using UnityEditor;
using UnityEngine;

public class ItemSand : Item
{
    public ItemSand(int stackAmount)
    {
        id = 3;
        itemName = "Sand";
        Stacks = stackAmount;
        maxStacks = 1;
        sprite = SpriteLibary.GetSprite(id);
    }
}