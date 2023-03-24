using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{

    public InventorySlot[] inventorySlots;
    public InventorySlot craftedItem;

    public Inventory invetory;

    public TextMeshProUGUI CraftingText;

    private bool craftFound = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Update()
    {
        CheckCraft();
    }

    public void CheckCraft()
    {
        if (inventorySlots[0].HeldItem != null && inventorySlots[1].HeldItem != null)
        {
            if (inventorySlots[0].HeldItem.itemName == "Rock" && inventorySlots[1].HeldItem.itemName == "Wood")
            {
                craftedItem.HeldItem = new ItemSand(1);
                if(!craftFound)
                {
                    InventoryManagerUI.instance.FillInventoryUI();
                }
                craftFound = true;
                return;
            }
            else
            {
                craftedItem.HeldItem = null;
            }
            craftFound = false;
        }
    }
    
    public void Craft()
    {
        if (inventorySlots[0].HeldItem != null && inventorySlots[1].HeldItem != null)
        {
            if (inventorySlots[0].HeldItem.itemName == "Rock" && inventorySlots[1].HeldItem.itemName == "Wood")
            {
                invetory.RemoveItem(inventorySlots[0].HeldItem);
                invetory.RemoveItem(inventorySlots[1].HeldItem);
                invetory.AddItem(new ItemSand(1));
                craftedItem.HeldItem = null;
                InventoryManagerUI.instance.FillInventoryUI();

            }
        }
    }
}
