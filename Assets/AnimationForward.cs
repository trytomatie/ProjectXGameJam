using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationForward : MonoBehaviour
{
    public CharacterController cc;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        print(anim.deltaPosition);
    }
}
