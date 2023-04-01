using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    public ParticleSystem particles;
    public LayerMask layerMask;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == layerMask)
        {
            particles.Play();
            Invoke("StopParticleStream", 0.3f);
        }
    }

    private void StopParticleStream()
    {
        particles.Stop();
    }
}
