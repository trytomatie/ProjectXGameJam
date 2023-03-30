using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatusManager : MonoBehaviour
{
    private int hp = 3;
    public int maxHp = 3;

    public UnityEvent deathEvent;


    public int Hp { get => hp;
        set 
        {
            if(value <= 0)
            {
                deathEvent.Invoke();
            }
            hp = value;
        }
    }
}
