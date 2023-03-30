using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabilizerController : MonoBehaviour
{
    private Rigidbody stabilizer;
    public Transform controller;
    public float minRagdollTime = 2;
    public Vector3 force;
    private float ragdollTime;
    public float standUpHeight = 0.3f;

    public bool ragdolling = false;
    // Start is called before the first frame update
    void Start()
    {
        stabilizer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ragdollTime < (Time.time - minRagdollTime) && !stabilizer.isKinematic && stabilizer.velocity.magnitude < 0.1f)
        {
            stabilizer.isKinematic = true;
            StartCoroutine(StandUp());

        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            AddForceToStabilizer();
        }
    }


    public void AddForceToStabilizer()
    {
        print("Force Added");
        stabilizer.isKinematic = false;
        ragdolling = true;
        stabilizer.AddForce(force, ForceMode.Impulse);
        stabilizer.transform.parent = null;
        ragdollTime = Time.time;
    }

    public IEnumerator StandUp()
    {
        float time = Time.time;
        Vector3 startRotation = stabilizer.transform.eulerAngles;
        float startYPos = stabilizer.transform.position.y;
        while (1 != Mathf.Clamp01((Time.time - time) / 2))
        {
            Vector3 rotation = Vector3.Lerp(startRotation, controller.eulerAngles, Mathf.Clamp01((Time.time - time) /2));
            Vector3 position = Vector3.Lerp(new Vector3(stabilizer.transform.position.x, startYPos, stabilizer.transform.position.z),
                new Vector3(stabilizer.transform.position.x, startYPos + standUpHeight, stabilizer.transform.position.z), Mathf.Clamp01((Time.time - time) / 2));
            stabilizer.transform.eulerAngles = rotation;
            stabilizer.transform.position = position;
            yield return new WaitForEndOfFrame();
        }


        if(controller.GetComponent<CharacterController>() != null)
        {
            controller.GetComponent<CharacterController>().enabled = false;
        }

        controller.transform.position = stabilizer.transform.position;

        if (controller.GetComponent<CharacterController>() != null)
        {
            controller.GetComponent<CharacterController>().enabled = true;
        }

        stabilizer.transform.parent = controller.transform;
        stabilizer.transform.localPosition = Vector3.zero;
        ragdolling = false;
    }

    public void IndefinteRagdoll()
    {
        minRagdollTime = 3000;
    }
}
