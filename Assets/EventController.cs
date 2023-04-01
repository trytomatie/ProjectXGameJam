using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    private Animator anim;
    private int counter;
    public int counterLimit;

    public UnityEvent counterEvent;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void SetFirstBool(bool value)
    {
        anim.SetBool("bool", value);
    }

    public void IncreaseCounter(int i)
    {
        counter += i;
        if(counter >= counterLimit)
        {
            counterEvent.Invoke();
        }

    }
}
