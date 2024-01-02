using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovementObsolete : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private float smoothingTime = 0.075f;
    private float rotateVelocity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                moveCharacter(hit.point, 0);
            }

            
        }

        
    }

    public void moveCharacter(Vector3 destination, float stoppingDistance)
    {
        agent.SetDestination(destination);

        Quaternion rotationToLookAt = Quaternion.LookRotation(destination - gameObject.transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, smoothingTime * Time.deltaTime);

        transform.eulerAngles = new Vector3(0, rotationY, 0);

        agent.stoppingDistance = stoppingDistance;
    }
}
