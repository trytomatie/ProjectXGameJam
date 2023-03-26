using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEasyAllInOne : MonoBehaviour
{
    private CharacterController plCharacterController;
    public Transform camer;
    public float turnSmoothTime = 0.1f;
    public float speed;
    public GameObject flashlight;
    private bool groundedPlayer;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float noise = 0;
    private Vector3 playerVelocity;
    public float chaseIndex=0;
    public LayerMask layerMask;
    public float timeAtAim = 0;
    public Transform aimAt; 

    public float playerHeight = 1.1f; // distance to cast the ground raycast
    public LayerMask groundLayer; // the layer(s) that the player can stand on

    private float verticalInput;
    private float horizontalInput;
    float turnSmoothVelocity;
    private GameObject lastAimedEnemy;
    public GameObject chaseStateVolume;
   
    // Start is called before the first frame update
    void Start()
    {
        plCharacterController = GetComponent<CharacterController>();
        chaseStateVolume.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        //check if player is on ground and Gravity and stuff
        Grounded();
        CheckWhatsAimedAt();
        CheckChaseState();

        //get Input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Get the camera's forward vector and flatten it
        Vector3 cameraForward = Vector3.Scale(camer.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = (horizontalInput * camer.right + verticalInput * cameraForward).normalized;

        //Move Character
        if (direction.magnitude >= 0.1f)
        {
            //create the viewing angle with movement and Camera angle
            
            //move
            plCharacterController.Move(direction * speed * Time.deltaTime);
        }
        
            //rotate with camera angle
            float targetAngle = camer.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        
    }

    private void CheckChaseState()
    {
        if (chaseIndex > 0)
        {
            chaseStateVolume.SetActive(true);
        } else
        {
            chaseStateVolume.SetActive(false);
        }
    }

    private void CheckWhatsAimedAt()
    {
        Vector3 aimDirection = aimAt.transform.position - transform.position; 
        Ray ray = new Ray(transform.position, aimDirection);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //sende Raycast
        //finde heraus ob Enemy
        //wenn enemy start 1 timer
        //if not enemy but last enemy not then reset last aimed enemy and countdown 0
        //if aimed at = last enemy increase countdown to 
        //if countdown > number than 

        //It would be smarter to have this Time aimed at on the Enemy itself but it is a prototype so who cares
        if (Physics.Raycast(ray, out RaycastHit hit, 40, layerMask))
        {
            if (hit.transform.gameObject.GetComponent<MouseStateManager>() != null)
            {
                if (hit.transform.gameObject == lastAimedEnemy)
                {
                    if (lastAimedEnemy.GetComponent<MouseStateManager>().blendAble && checkFaceOffEnemy() )
                    {
                        timeAtAim += 1 * Time.deltaTime;
                        if (timeAtAim > 2)
                        {
                            lastAimedEnemy.GetComponent<MouseStateManager>().SwitchMouseState(lastAimedEnemy.GetComponent<MouseStateManager>().mouseyBlended);
                            timeAtAim = 0;
                        }
                    }
                } else if (hit.transform.gameObject != lastAimedEnemy)
                {
                    lastAimedEnemy = hit.transform.gameObject;
                    timeAtAim = 0;
                }
            } 

            /*
            if (hit.transform.gameObject!= lastAimedEnemy) ///hier arbeite ich
            {
                 
                if (hit.transform.gameObject.GetComponent<MouseStateManager>()!=null)
                {
                    aimInterruptTime = 2f;
                }
            }
            // A collider was hit
            Debug.Log("Hit object: " + hit.transform.name);
            // Do something with the hit object
        }
        else
        {
            if (aimInterruptTime > 0 && lastAimedEnemy)
            {
                aimInterruptTime -= (1*Time.deltaTime);
            }
            else if (aimInterruptTime > 0)
            {
                aimInterruptTime = 0;
                lastAimedEnemy = null;
            }

            // No collider was hit
            Debug.Log("No object hit");*/
        } else
        {
            if (timeAtAim > 0)
            {
                timeAtAim -= 1 * Time.deltaTime;
            }
        }

    }

    private bool checkFaceOffEnemy()
    {
        //create Vector between enemy and Character
        Vector3 vectorBetween = transform.position - lastAimedEnemy.transform.position;
        //calculate the angle
        float angle = Vector3.Angle(vectorBetween, lastAimedEnemy.transform.forward);
        if (lastAimedEnemy.GetComponent<MouseStateManager>().mouseyFieldOfView > angle)
        {
            Debug.Log("FaceOff");
            return true;
        }
        else return false;
    }

    private void Grounded()
    {
        // cast a raycast downwards from the object's position
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight, groundLayer))
        {
            groundedPlayer = true;
        }
        else
        {
            groundedPlayer = false;
        }

        

        //add Gravity 

        //if the player is on the ground and his downward movement is not 0 jet change it to 0
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // if the player presses the Jump Button and the Character is on the ground than Jump

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        plCharacterController.Move(playerVelocity * Time.deltaTime);
    }
}

