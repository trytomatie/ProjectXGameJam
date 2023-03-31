using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventTrigger : MonoBehaviour
{
    public UnityEvent activateTrigger;
    public UnityEvent deactivateTrigger;

    public void ActivateTrigger()
    {
        activateTrigger.Invoke();
    }

    public void DeactivateTrigger()
    {
        deactivateTrigger.Invoke();
    }
}
