using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    
    [SerializeField]
    private float horizontalSpeed = 2;
    [SerializeField]
    private float verticalSpeed = 2;
    [SerializeField]
    private float smoothingTime = 0.075f;

    private bool canInputMove = true;
    private NavMeshAgent agent;
    private Vector3 currentVelocity;
    private float rotateVelocity;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentVelocity = Vector3.zero;
        lockRotation();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canInputMove)
            return;
        //here we can check for stun, animation lock, etc
        move();

    }


    private void move()
    {
        agent.velocity = Vector3.zero;
        float verticalInput = 0;
        float horizontalInput = 0;
        if (Input.GetKey(KeyCode.W))
            verticalInput += 1;
        if (Input.GetKey(KeyCode.S))
            verticalInput -= 1;
        if (Input.GetKey(KeyCode.D))
            horizontalInput += 1;
        if (Input.GetKey(KeyCode.A))
            horizontalInput -= 1;

        //the vector should be transformed according to the camera, ill bother with it later
        Vector3 direction = Vector3.Normalize(new Vector3(-verticalInput * verticalSpeed, 0, horizontalInput * horizontalSpeed));
        
        agent.velocity = direction * (horizontalSpeed + verticalSpeed) / 2;
        //transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        if (direction == Vector3.zero)
            return;
        Quaternion rotation = Quaternion.LookRotation(direction);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation.eulerAngles.y, ref rotateVelocity, smoothingTime * Time.deltaTime);

        transform.eulerAngles = new Vector3(0, rotationY, 0);

        

    }

    public void lookAtCursor()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            lookAt(hit.point);
        }
    }

    public void lookAt(Vector3 point)
    {

        transform.rotation = Quaternion.LookRotation(point - transform.position);

    }


    //doing these in the attack interval instead as an animation event might be a better idea for more responsiveness
    public void enableInputMovement()
    {
        canInputMove = true;
    }

    public void disableInputMovement()
    {
        canInputMove = false;
    }

    public void lockRotation()
    {
        agent.updateRotation = false;
    }

    public void unlockRotation()
    {
        agent.updateRotation = true;    
    }
}
