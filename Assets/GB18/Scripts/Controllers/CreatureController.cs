using UnityEngine;


[RequireComponent(typeof(Health))]
public class CreatureController : MonoBehaviour
{
    [SerializeField]
    private string deathTrigger;
    [SerializeField]
    private string movementTrigger;
    [SerializeField]
    private string hitTrigger;

    private Animator animator;
    private bool hasAnimator;

    private Health health;
    private bool canTakeDamage = true;

    private CreatureMovement creatureMovement;
    private bool hasMovement;

    private void Awake()
    {
        health = GetComponent<Health>();
        if (TryGetComponent(out CreatureMovement tryCreatureMovement))
        {
            creatureMovement = tryCreatureMovement;
            hasMovement = true;
        }
        else
        {
            hasMovement = false;
        }

        if (TryGetComponent(out Animator tryAnimator))
        {
            animator = tryAnimator;
            hasAnimator = true;
        }
        else
        {
            hasAnimator = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (hasAnimator)
        {
            animator.SetTrigger(hitTrigger);
        }

        if (canTakeDamage)
        {
            health.GetHit(damage);
            canTakeDamage = false;
        }

        if (!health.CheckIsAlive()) { 
            animator.SetTrigger(deathTrigger);
        }
    }

    public void SetCanTakeDamage() {
        this.canTakeDamage = true;
    }

    public void Move(bool isMoving, float direction, bool isJump)
    {
        if (!hasMovement)
        {
            return;
        }

        if (hasAnimator)
        {
            animator.SetBool(movementTrigger, isMoving);
        }

        creatureMovement.Move(isMoving, direction, isJump);
    }
}
