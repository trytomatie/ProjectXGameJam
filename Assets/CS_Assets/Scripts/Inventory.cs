using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int inventorySize = 6;
    public Item[] inventory;

    public float test = 0;

    private void Start()
    {
        inventory = new Item[inventorySize];
        AddItem(new ItemRock(1));
        AddItem(new ItemWood(1));
    }

    public bool AddItem(Item item)
    {
        // look through all itemslots
        for (int i = 0; i < inventory.Length;i++)
        {
            // if item is already in there and it has space left on it's stack, add to stack
            if (inventory[i] != null && inventory[i].itemName == item.itemName && inventory[i].Stacks != inventory[i].maxStacks)
            {
                int stackAmount = item.Stacks + inventory[i].Stacks;
                if(stackAmount <= item.maxStacks)
                {
                    inventory[i].Stacks = stackAmount;
                    return true;
                }
                else
                {
                    inventory[i].Stacks = item.maxStacks;
                    item.Stacks = stackAmount - item.maxStacks;
                    AddItem(item);
                    return true;
                }                
            }    
        }
        // if it's not there, add it to inventory
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            { 
                inventory[i] = item;
                return true;
            }
        }
        print("Inventory full");
        return false;
    }

    public bool RemoveItem(Item item)
    {
        // Remove requested Item and Ammount of stacks from the Inventory

        // look through all itemslots
        for (int i = 0; i < inventory.Length; i++)
        {
            // if item is found, Remove stacks
            if (inventory[i] != null && inventory[i].itemName == item.itemName)
            {
                int stackAmount = inventory[i].Stacks - item.Stacks;
                switch(stackAmount)
                {
                    case < 0:
                        item.Stacks = Math.Abs(stackAmount);
                        inventory[i] = null;
                        RemoveItem(item);
                        break;
                    case 0:
                        inventory[i] = null;
                        break;
                    default:
                        inventory[i].Stacks = stackAmount;
                        break;
                }
                return true;
            }
        }
        return false;
    }

    public void SwapItemPositions(int i, int o)
    {
        Item item1 = inventory[i];
        Item item2 = inventory[o];

        inventory[i] = item2;
        inventory[o] = item1;
    }

}

