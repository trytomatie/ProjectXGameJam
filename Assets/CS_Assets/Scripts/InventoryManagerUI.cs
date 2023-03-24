using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerUI : MonoBehaviour
{
    public Inventory targetInventory;
    private static Inventory staticInventory;
    public InventorySlot[] inventorySlots;
    public static InventoryManagerUI instance;
    // Start is called before the first frame update
    void Start()
    {
        staticInventory = targetInventory;
        instance = this;
        FillInventoryUI();
    }

    public static Inventory CurrentInventory()
    {
        return staticInventory;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            FillInventoryUI();
        }
    }

    public void FillInventoryUI()
    {
        for(int i = 0; i < targetInventory.inventory.Length;i++)
        {
            inventorySlots[i].HeldItem = targetInventory.inventory[i];
        }
    }

    public int GetInvenoryPosition(InventorySlot searchedSlot)
    {
        int i = 0;
        foreach(InventorySlot slot in inventorySlots)
        {
            if(slot == searchedSlot)
            {
                return i;
            }
            i++;
        }
        return -1;
    }
}
