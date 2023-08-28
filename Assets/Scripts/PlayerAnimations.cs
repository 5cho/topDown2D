using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    private Direction playerDirection;
    private Vector3 playerMovement;
    private Vector3 lastPlayerMovement;

    public const string IS_MOVING = "IsMoving";
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
    public const string IS_ATTACKING = "IsAttacking";

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        playerController.OnAttackActionPerformed += PlayerController_OnAttackActionPerformed;
    }

    private void PlayerController_OnAttackActionPerformed(object sender, System.EventArgs e)
    {
        animator.SetTrigger(IS_ATTACKING);
    }

    private void Update()
    {
        playerMovement = playerController.GetMovementVectorNomalized();
        if(playerMovement != Vector3.zero)
        {
            lastPlayerMovement = playerMovement;
        }
        GetDirection(playerMovement);
        HandleAnimations();
    }
    private Direction GetDirection(Vector3 movementVector)
    {
        if(movementVector.x > 0)
        {
            playerDirection = Direction.Right;
        }
        else if(movementVector.x < 0)
        {
            playerDirection = Direction.Left;
        }
        else if(movementVector.y < 0)
        {
            playerDirection = Direction.Down;
        }
        else if(movementVector.y > 0)
        {
            playerDirection = Direction.Up;
        }

        return playerDirection;
    }
    private void HandleAnimations() 
    {
        if (!playerController.IsMoving())
        {
            animator.SetBool(IS_MOVING, false);
            animator.SetFloat(HORIZONTAL, lastPlayerMovement.x);
            animator.SetFloat(VERTICAL, lastPlayerMovement.y);
        }
        else
        {
            animator.SetBool(IS_MOVING, true);
            animator.SetFloat(HORIZONTAL, playerMovement.x);
            animator.SetFloat(VERTICAL, playerMovement.y);
        }

    }

}
