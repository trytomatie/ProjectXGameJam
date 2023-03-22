using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleUI : MonoBehaviour
{
    public Transform target;

    private RectTransform myTransform;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        myTransform.transform.position = mainCamera.WorldToScreenPoint(target.position);
    }
}
