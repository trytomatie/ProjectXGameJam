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
    public float aimInterruptTime = 0;

    public float playerHeight = 1.1f; // distance to cast the ground raycast
    public LayerMask groundLayer; // the layer(s) that the player can stand on

    private float verticalInput;
    private float horizontalInput;
    float turnSmoothVelocity;
    private GameObject lastAimedObject;
   
    // Start is called before the first frame update
    void Start()
    {
        plCharacterController = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //check if player is on ground and Gravity and stuff
        Grounded();
        CheckWhatsAimedAt();

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

    private void CheckWhatsAimedAt()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            if (hit.transform.gameObject!= lastAimedObject)
            {
                lastAimedObject = hit.transform.gameObject;
                if (lastAimedObject.GetComponent<MouseStateManager>()!=null)
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
            if (aimInterruptTime > 0)
            {
                aimInterruptTime -= (1*Time.deltaTime);
            }
            else

            // No collider was hit
            Debug.Log("No object hit");
        }

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

        // do something with the result, like play an animation or sound, or adjust the object's movement
        if (groundedPlayer)
        {
            Debug.Log("Grounded!");
        }
        else
        {
            Debug.Log("Not grounded!");
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

