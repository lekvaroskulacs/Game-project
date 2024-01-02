using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombatObsolete : MonoBehaviour
{
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private float attackRange = 2f;
    [SerializeField]
    private GameObject targetedEnemy;


    private CharacterMovementObsolete characterMovement;
    private enum AttackType { melee };
    private AttackType attackType = AttackType.melee;
    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GetComponent<CharacterMovementObsolete>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, enemyLayer.value))
            {
                targetedEnemy = hit.collider.gameObject;
                attack();
            }
            else
                targetedEnemy = null;
        }
    }

    public void attack()
    {
        if (attackType == AttackType.melee)
        {
            if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > attackRange)
            {
                characterMovement?.moveCharacter(targetedEnemy.transform.position, attackRange);
            }
        }
    }
}
