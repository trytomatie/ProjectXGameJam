using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schalter : InteractWithLight
{
    public Material inActiveMat;
    public Material activeMat;
    public LayerMask layer;

    private Renderer objectRenderer;
    private Collider objectCollider;
    public GameObject door;
    private Vector3 doorStart;
    bool active;
    

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();

        //objectCollider.enabled = false;
        objectCollider.isTrigger = true;
        objectRenderer.material = inActiveMat;
        doorStart = door.transform.position;
    }

    private void Update()
    {
        if (active)
        {
            CheckStandOn();
        }
    }

    private void CheckStandOn()
    {

        if (Physics.BoxCast(transform.position, Vector3.one, transform.up, out RaycastHit hitInfo, Quaternion.identity, 
            2, layer))
        {
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 20, door.transform.position.z);
        }
    }

    public override void EnterLight()
    {
        Debug.Log("MatChangeAndGetSolidEnter");
        objectCollider.isTrigger = false;
        objectRenderer.material = activeMat;
        active = true;
    }

    public override void LooseLight()
    {
        Debug.Log("MatChangeAndGetSolidLoose");
        objectCollider.isTrigger = true;
        objectRenderer.material = inActiveMat;
        active = false;
    }
}
