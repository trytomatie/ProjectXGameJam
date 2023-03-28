using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAimHelperScript : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject AimAtHelper;
    //public Camera mainCamera;
    public float distance = 10f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // Object was hit
            //Debug.Log("Hit object: " + hit.transform.name);
            AimAtHelper.transform.position = hit.point;
        }
        else
        {
            // Object was not hit
            //Debug.Log("No object hit");
            Vector3 targetPosition = ray.origin + ray.direction * distance;
            AimAtHelper.transform.position = targetPosition;
        }
    }
}
