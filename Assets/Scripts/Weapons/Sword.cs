using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//okay this might be generally good for all weapons xddd
public class Sword : MonoBehaviour
{
    [SerializeField]
    private GameObject collisionSphere;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void createCollisionSphere()
    {
        Instantiate(collisionSphere, player.transform.position, new Quaternion());
    }
}
