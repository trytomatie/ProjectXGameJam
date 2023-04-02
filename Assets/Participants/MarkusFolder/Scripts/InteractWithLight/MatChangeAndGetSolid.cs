using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChangeAndGetSolid : InteractWithLight
{
    public Material inActiveMat;
    public Material activeMat;

    private Renderer objectRenderer;
    private Collider objectCollider;

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();

        //objectCollider.enabled = false;
        objectCollider.isTrigger = true;
        objectRenderer.material = inActiveMat;
    }

    public override void EnterLight()
    {
        Debug.Log("MatChangeAndGetSolidEnter");
        objectCollider.isTrigger = false;
        objectRenderer.material = activeMat;
        
    }

    public override void LooseLight()
    {
        Debug.Log("MatChangeAndGetSolidLoose");
        objectCollider.isTrigger = true;
        objectRenderer.material = inActiveMat;

    }
}
