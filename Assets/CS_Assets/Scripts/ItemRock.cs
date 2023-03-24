using UnityEditor;
using UnityEngine;

public class ItemRock : Item
{
    public ItemRock(int stackAmount)
    {
        id = 1;
        itemName = "Rock";
        Stacks = stackAmount;
        maxStacks = 1;
        sprite = SpriteLibary.GetSprite(id);
    }
}