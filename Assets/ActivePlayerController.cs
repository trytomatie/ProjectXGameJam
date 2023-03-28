using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerController : MonoBehaviour
{

    public float charcterGuidanceSpeed = 10;
    public Vector3 offset;
    private float movementSpeed;
    private Camera mainCamera;

    public float radius = 0.2f;
    public float force = 200;

    private Vector3 lastMovement;
    private Rigidbody ccRb;
    public Rigidbody hip;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float targetSpeed = charcterGuidanceSpeed;
        float acceleration = 55;
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        print(movement);
        // Vector3 cameraDependingMovement = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * movement;
        Vector3 cameraDependingMovement = movement;
        movementSpeed = Mathf.MoveTowards(movementSpeed, movement.magnitude * targetSpeed, acceleration * Time.deltaTime);

        if (verticalInput != 0 || horizontalInput != 0)
        {
            lastMovement = cameraDependingMovement;
        }

        if(verticalInput == 0 && horizontalInput == 0)
        {
           // cc.transform.localPosition = Vector3.zero;
        }

        hip.velocity = (lastMovement * movementSpeed * Time.deltaTime);
        //cc.transform.localPosition = new Vector3(Mathf.Clamp(cc.transform.localPosition.x, -radius, radius), Mathf.Clamp(cc.transform.localPosition.y, -radius, radius), Mathf.Clamp(cc.transform.localPosition.z, -radius, radius));
        MoveActiveRagdroll();
    }

    public void MoveActiveRagdroll()
    {
        // Vector3 direction = ((cc.transform.position + offset) - hip.transform.position).normalized;
        // 
    }

}
