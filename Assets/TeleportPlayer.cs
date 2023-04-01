using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportDestination;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "PlayerStabilizer")
        {
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = teleportDestination.position;
            other.GetComponent<CharacterController>().enabled = true;
        }
    }
}
