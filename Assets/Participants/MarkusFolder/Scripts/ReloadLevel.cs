using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (CompareTag("Player"))
        {
            collision.gameObject.transform.position = spawnPoint.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = spawnPoint.position;
    }
}
