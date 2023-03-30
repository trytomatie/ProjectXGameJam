using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float damage = 5;
    [HideInInspector]
    public Collider weaponCollider;
    public bool lightDamage = false;

    [HideInInspector]
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            player.GetComponent<PlayerMainScipt>().DealDamage(other, damage, lightDamage);
            Debug.Log("Hit Enemy");
        }
    }
}
