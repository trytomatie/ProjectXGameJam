using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    public Transform spawnPoint;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update SpawnPoint" +
            "alslsls");
        if (Input.GetKeyDown(KeyCode.L))
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = spawnPoint.position;
           player.GetComponent<CharacterController>().enabled = true;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggererd");
        //musst den character controller ausschalten
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = spawnPoint.position;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
