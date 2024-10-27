using UnityEngine;

[RequireComponent(typeof(CreatureController))]
public class InputController : MonoBehaviour
{

    private CreatureController creatureController;
    private Shooter shooter;
    private float horizontalDirection;

    private void Awake()
    {
        horizontalDirection = 1;
        creatureController = GetComponent<CreatureController>();
        shooter = GetComponent<Shooter>();
    }

    void Update()
    {
        bool isHorizontalMove = Input.GetButton(InputKeyBinds.HORIZONTAL_AXIS);
        bool isJump = Input.GetButton(InputKeyBinds.JUMP_BUTTON);
        bool isShoot = Input.GetButtonDown(InputKeyBinds.FIRE);

        if (isHorizontalMove)
        {
            horizontalDirection = Input.GetAxisRaw(InputKeyBinds.HORIZONTAL_AXIS);
        }

        creatureController.Move(isHorizontalMove, Input.GetAxis(InputKeyBinds.HORIZONTAL_AXIS), isJump);

        if (isShoot)
        {
            shooter.Shoot(horizontalDirection);
        }
    }
}
