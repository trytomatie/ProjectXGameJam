using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler
{
    public Item heldItem;
    public Image imageComponent;
    public TextMeshProUGUI itemCount;

    public TextMeshProUGUI title;
    public TextMeshProUGUI desciption;

    private static InventorySlot dragge = null;
    private static Vector3 initialImagePosition;
    
    public void OnDrag(PointerEventData eventData)
    {
        print(gameObject.name);
        imageComponent.rectTransform.position = Input.mousePosition;
        dragge = this;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        dragge.imageComponent.rectTransform.localPosition = Vector3.zero;
        GameObject target = eventData.pointerCurrentRaycast.gameObject;
        if (target.GetComponent<InventorySlot>() != null && eventData.pointerCurrentRaycast.gameObject != gameObject)
        {
            InventorySlot targetSlot = target.GetComponent<InventorySlot>();
            Item draggeItem = dragge.HeldItem;
            dragge.HeldItem = targetSlot.HeldItem;
            targetSlot.HeldItem = draggeItem;
            print("Droped on" + gameObject.name);
            InventoryManagerUI.CurrentInventory().SwapItemPositions(InventoryManagerUI.instance.GetInvenoryPosition(target.GetComponent<InventorySlot>()), InventoryManagerUI.instance.GetInvenoryPosition(this));
        }
        dragge = null;
        InventoryManagerUI.instance.FillInventoryUI();
    }

    public Item HeldItem 
    { 
        get => heldItem;
        set
        {
            if(heldItem != value)
            {
                if(value != null)
                {
                    UnsubscribeToProperties();
                    imageComponent.sprite = value.sprite;
                    itemCount.text = value.Stacks.ToString();
                    heldItem = value;
                    SubscribeToProperties();
                }
                else
                {
                    imageComponent.sprite = null;
                    itemCount.text = "";
                }
            }
        } 
    }

    private void SubscribeToProperties()
    {
        HeldItem.StackEvent += UpdateStackText;
    }

    private void UnsubscribeToProperties()
    {
        HeldItem.StackEvent -= UpdateStackText;
    }

    public void UpdateStackText(object sender,EventArgs e)
    {
        itemCount.text = HeldItem.Stacks.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (heldItem != null)
        {
            title.text = heldItem.itemName;
            desciption.text = heldItem.itemName;
        }
    }
}
