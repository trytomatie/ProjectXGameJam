using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

/// <summary>
/// Markus Schwalb
/// This is the state mashine that runsthe Mouse states
/// </summary>

public class MouseStateManager : MonoBehaviour
{
    //public Volume chaseVolume;

    //States
    public MousePatrolState mousePatrol = new MousePatrolState();
    public MouseIdleState mouseIdle = new MouseIdleState();
    public MouseAnimationState mouseAni = new MouseAnimationState();
    public MouseyChase mChase = new MouseyChase();
    public MouseCheeseState mouseCheese = new MouseCheeseState();
    public MouseySearchState mouseySearch = new MouseySearchState();

    public MouseBaseState currentState;


    public AudioSource mouth;
    public AudioClip[] voiceLines;

    //Components
    [HideInInspector]
    public Animator mouseAnimator;
    [HideInInspector]
    public NavMeshAgent navMeshMouseAgent;

    //Referenced Objects
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject cheese;
    public GameObject lookAt;

    public Transform[] patrolPoints;
    public int nextPatrolPoint;
    
    //Bools to play with
    [HideInInspector]
    public bool forward;
    [HideInInspector]
    public bool inChase;
    //public bool distracted;

    //Parameters
    public LayerMask mouseRayCastLayers;
    public float mouseyFieldOfView;
    public float eyeHeight;
    public float mouseyViewingDistance;
    public float catchDistance = 1.35f;
    public float viewHeight = 1;



    

    // Start is called before the first frame update
    void Awake()
    {
        currentState = mouseIdle;
        currentState.EnterMouseState(this);
        

        //chaseVolume = GameObject.Find("ChaseVolume").GetComponent<Volume>();

        //initialize Parameter 
        navMeshMouseAgent = GetComponent<NavMeshAgent>();
        mouseAnimator = GetComponent<Animator>();
        player = GameObject.Find("Clyde The Kid");

        nextPatrolPoint = 0;
        forward = true;
        inChase = false;
        // distracted = false;
        //SwitchMouseState(mouseySearch);
    }

    // Update is called once per frame
    void Update()
    {

        //Update current state
        currentState.UpdateMouseState(this);

        //update the animations
        mouseAni.UpdateMouseState(this);
    }


    /// <summary>
    /// this method carrys out hte exit state of the currenstate than loads the next state and plays the enter state of the new state
    /// </summary>
    /// <param name="newMouseState"></param>
    public void SwitchMouseState(MouseBaseState newMouseState)
    {
        currentState.ExitMouseState(this);
        currentState = newMouseState;
        currentState.EnterMouseState(this);
    }

    /// <summary>
    /// Play Voice Lines
    /// </summary>
    /// <param name="line"></param>

    public void PlayVoiceLines(AudioClip line)
    {
        if (!mouth.isPlaying)
        {
            mouth.clip = line;
            mouth.Play();
        }
    }

    /// <summary>
    /// trigger on trigger enter (use case is for cheese)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
    }

    
}
