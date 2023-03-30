using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHelperScript : MonoBehaviour
{
    private GameObject parentObject;
    // Start is called before the first frame update
    void Start()
    {
        parentObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackStart() 
    { 
        if (parentObject.GetComponent<PlayerMainScipt>() != null)
        {
            parentObject.GetComponent<PlayerMainScipt>().AttackStart();
        }
    }

    public void AttackEnd()
    {
        if (parentObject.GetComponent<PlayerMainScipt>() != null)
        {
            parentObject.GetComponent<PlayerMainScipt>().AttackEnd();
        }
    }
}
