using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    [SerializeField]
    private LayerMask treeLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        treeClick();
    }

    private void treeClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
           
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, treeLayer.value))
            {
                GameObject tree = hit.collider.gameObject;
                tree.SetActive(false);
            }

        }
    }
}
