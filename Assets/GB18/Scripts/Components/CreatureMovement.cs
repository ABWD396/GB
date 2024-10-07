using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CreatureMovement : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;

    private bool canJump = false;

    private Rigidbody2D rb;
    [SerializeField]
    private CapsuleCollider2D capsuleCollider;
    [SerializeField]
    private AnimationCurve curve;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        canJump = Physics2D.OverlapCapsule(capsuleCollider.transform.position, capsuleCollider.size * 0.2f, capsuleCollider.direction, 0);
    }

    public void Move(bool isMoving, float direction, bool isJump)
    {
        if (isJump)
        {
            Jump();
        }

        if (isMoving)
        {
            rb.velocity = new Vector2(curve.Evaluate(direction) * speed, rb.velocity.y);

            if (direction >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

    }

    public void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
