using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using System;

public class InteractionHandler : MonoBehaviour
{

    private Animator anim;

    private Interactable reachableInteractable;
    public bool canInteract = false;
    public float interactionDistance = 0.7f;
    public float interactionAngleThreshold = 45;
    public Transform itemAnchor;

    private List<Interactable> potentialInteractables = new List<Interactable>();
    private bool isInteracting = false;
    private Camera mainCamera;

    public GameObject canInteractUI;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        CheckForInteraction();
    }

    internal void RemoveInteractable(Interactable_Item interactable_Item)
    {
        potentialInteractables.Remove(interactable_Item);
    }

    /// <summary>
    /// Checks if an Interaction is available
    /// </summary>
    private void CheckForInteraction()
    {
        if(potentialInteractables.Count > 0)
        {

            ReachableInteractable = potentialInteractables.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).First();
            if (ReachableInteractable != null && Helper.DistanceBetween(gameObject, ReachableInteractable.gameObject) > interactionDistance && Helper.AngleBetween(gameObject, ReachableInteractable.gameObject) < interactionAngleThreshold)
            {
                canInteractUI.SetActive(true);
                canInteract = true;
            }
            else
            {
                canInteractUI.SetActive(false);
                canInteract = false;
            }
        }
    }

    /// <summary>
    /// Method that gets called when an object enters the InteractionTrigger
    /// </summary>
    /// <param name="other"></param>
    public void EnterTrigger(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            if (!isInteracting && !potentialInteractables.Contains(interactable))
            {
                potentialInteractables.Add(interactable);
                interactable.IsReachable = true;
            }
        }

    }
    /// <summary>
    /// Method that gets called when an object leaves the InteractionTrigger
    /// </summary>
    /// <param name="other"></param>
    public void ExitTrigger(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            Interactable interactable = other.gameObject.GetComponent<Interactable>();
            if (!isInteracting)
            {
                potentialInteractables.Remove(other.gameObject.GetComponent<Interactable>());
                interactable.IsReachable = false;
            }
        }
    }




    public Interactable ReachableInteractable 
    { get => reachableInteractable;
        set 
        {
            if(reachableInteractable != value && reachableInteractable != null)
            {
            }
            reachableInteractable = value;
        } 
    }
}
