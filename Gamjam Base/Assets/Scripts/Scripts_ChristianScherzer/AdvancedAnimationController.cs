using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedAnimationController : MonoBehaviour
{
    public List<Animator> targetAnimators;

    public void TriggerAnimationEvent(AnimationEvent ae)
    {
        targetAnimators[ae.intParameter].SetTrigger(ae.stringParameter);
    }
}
