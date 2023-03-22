using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_KeycardPanel : Interactable
{
    public Animator anim;
    private SoundManager sm;
    private void Start()
    {
        sm = GetComponent<SoundManager>();
        interactionName = "Interact";
    }

    public override void Interaction(GameObject source)
    {
        Inventory inventory = source.GetComponent<Inventory>();
        if(inventory.Keys > 0)
        {
            sm.SoundEvent("522329__tissman__hightech-confirm1");
            inventory.Keys--;
            anim.SetTrigger("action");
            enabled = false;
        }
        else
        {
            sm.SoundEvent("572936__bloodpixelhero__error");
        }

    }

    public override void TriggerAnimation(GameObject source)
    {
        source.GetComponent<Animator>().SetTrigger("interact");
    }
}

