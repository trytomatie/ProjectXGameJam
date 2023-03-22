using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.Rotate(new Vector3(0, -180, 0));
    }
}
