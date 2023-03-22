using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalator : MonoBehaviour
{
    public GameObject stairPice;
    public int pieces = 10;

    public int speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < pieces;i++)
        {
            GameObject go = Instantiate(stairPice, transform.position, transform.rotation, transform);
            go.GetComponent<Animator>().SetFloat("Offset", (float)i / (float)pieces);
            go.GetComponent<Animator>().SetFloat("Speed", speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
