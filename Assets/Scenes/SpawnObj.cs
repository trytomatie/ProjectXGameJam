using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        GameObject obj=  Instantiate(prefab, spawnPoints[Random.Range(0, 3)].transform.position, Quaternion.identity);
        Destroy(obj, 20f);
    }
}
