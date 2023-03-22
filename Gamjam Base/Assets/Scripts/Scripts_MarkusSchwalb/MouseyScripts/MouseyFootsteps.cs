using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Schwalb Markus
/// Play Footsteps
/// </summary>
public class MouseyFootsteps : MonoBehaviour
{
    public GameObject audioSourceObj;
    public AudioClip footsteps;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = audioSourceObj.GetComponent<AudioSource>();
        audioSource.clip = footsteps;
    }

    public void Step(AnimationEvent ae)
    {
        if (ae.animatorClipInfo.weight > 0.5)
        {
            
            audioSource.PlayOneShot(footsteps);
        }
    }

}
