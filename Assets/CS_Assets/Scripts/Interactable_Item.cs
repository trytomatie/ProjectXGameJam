using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Item : Interactable
{
    public string item;
    public Item itemInstnace;
    private void Start()
    {
        switch(item)
        {
            case "Rock":
                itemInstnace = new ItemRock(1);
                break;
            case "Wood":
                itemInstnace = new ItemWood(1);
                break;
            case "Sand":
                itemInstnace = new ItemSand(1);
                break;
        }
        interactionName = "Pick up";
    }

    public override void Interaction(GameObject source)
    {

        Inventory inventory = source.GetComponent<Inventory>();
        inventory.AddItem(itemInstnace);
        Destroy(gameObject);
        source.GetComponent<InteractionHandler>().RemoveInteractable(this);
        source.GetComponent<InteractionHandler>().canInteract = false;
        source.GetComponent<InteractionHandler>().canInteractUI.SetActive(false);
    }

    public override void TriggerAnimation(GameObject source)
    {
        source.GetComponent<Animator>().SetTrigger("grab");
    }
}

