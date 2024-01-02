using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    [SerializeField]
    private float meleeIntervalSeconds = 0.1f;
    [SerializeField]
    private float chainTimeLimit = 0.5f;
    [SerializeField]
    private Sword sword;

    private MeleeAttackStatus meleeAttackStatus = MeleeAttackStatus.able;
    private bool onMeleeCooldown = false;
    private CharacterMovement characterMovement;
    private float chainTime = 0;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {

        meleeAttack();

    }

    private void meleeAttack()
    {

        if (Input.GetMouseButtonDown(0))
        {

            if (onMeleeCooldown || meleeAttackStatus == MeleeAttackStatus.unable)
            {
                //Debug.Log("Can't attack now!");
                return;
            }
            
            StartCoroutine(meleeInterval());

            if (chainTime <= 0)
            {
                StartCoroutine(chainTimer());
                
            }
            else
                chainTime = chainTimeLimit;

            characterMovement.lookAtCursor();

        }

    }

    private void stateTransition()
    {
        switch (meleeAttackStatus)
        {
            case MeleeAttackStatus.able:

                //Debug.Log("First attack!");

                meleeAttackStatus = MeleeAttackStatus.second;
                break;

            case MeleeAttackStatus.second:

                //Debug.Log("Second attack!");

                meleeAttackStatus = MeleeAttackStatus.third;
                break;

            case MeleeAttackStatus.third:

                //Debug.Log("Third attack!");

                meleeAttackStatus = MeleeAttackStatus.able;
                break;
        }
    }

    private IEnumerator meleeInterval()
    {
        onMeleeCooldown = true;
        characterMovement.disableInputMovement();
        characterMovement.lockRotation();
        animationTrigger();        

        yield return new WaitForSeconds(meleeIntervalSeconds);

        characterMovement.enableInputMovement();
        characterMovement.unlockRotation();
        stateTransition();
        animator.SetTrigger("AttackCancel");
        onMeleeCooldown = false;
    }

    private IEnumerator chainTimer()
    {

        chainTime = chainTimeLimit;

        if (meleeAttackStatus == MeleeAttackStatus.able)
        {
            chainTime = 0;
            yield break;
        }

        while (chainTime > 0)
        {
            chainTime -= Time.deltaTime;
            yield return null;
        }

        meleeAttackStatus = MeleeAttackStatus.able;

    }


    private void animationTrigger()
    {
        switch (meleeAttackStatus)
        {
            case MeleeAttackStatus.able:
                animator.SetTrigger("MeleeAttack");
                break;

            case MeleeAttackStatus.second:
                animator.SetTrigger("SecondAttack");
                break;

            case MeleeAttackStatus.third:
                animator.SetTrigger("BigMeleeAttack");
                break;

            default:
                break;
        }

    }
    
    public void swordCollider()
    {
        sword.createCollisionSphere();
    }

}
