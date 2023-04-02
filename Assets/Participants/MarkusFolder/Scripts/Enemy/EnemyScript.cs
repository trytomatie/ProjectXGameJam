using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 100;
    public float lightDamageMultipl = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(float damage,bool lightDamage)
    {
        //wenn die Waffe Lichtschaden macht mach Multiplikator drauf
        if (lightDamage)
        {
            health -= damage * lightDamageMultipl;
        }
        else
        {
            health -= damage;
            Debug.Log("Enemy got Damage " + damage);
        }
        CheckLive();

    }

    public void GetDamage(float damage, bool lightDamage, GameObject target)
    {
        //wenn die Waffe Lichtschaden macht mach Multiplikator drauf
        if (lightDamage)
        {
            health -= damage * lightDamageMultipl;
        }
        else
        {
            health -= damage;
            Debug.Log("Enemy got Damage " + damage);
        }
        CheckLive();

        Vector3 direction = target.transform.position - transform.position;

        // Calculate the rotation to look at the target
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Apply the rotation to the object
        transform.rotation = rotation;
    }

    public void CheckLive()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
