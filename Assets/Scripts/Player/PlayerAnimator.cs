using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    PlayerMovement playerMovement;

    Animator animator;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
    }

    void Start()
    {
        playerMovement.OnMoveStartEvent += PlayerMovement_OnMoveStart;
        playerMovement.OnMoveCompleteEvent += PlayerMovement_OnMoveComplete;
    }

    void PlayerMovement_OnMoveStart(object sender, Vector2 direction)
    {
        animator.SetBool("IsWalking", true);

        if (direction.y > 0) // Move Up
        {
            animator.SetBool("IsFacingSouth", false);
        }
        else if (direction.y < 0) // Move Down
        {
            animator.SetBool("IsFacingSouth", true);
        }
        else if (direction.x > 0) // Move Right
        {
            animator.SetBool("IsFacingEast", true);
        }
        else if (direction.x < 0) // Move Left
        {
            animator.SetBool("IsFacingEast", false);
        }

        // animator.SetBool("IsFacingRight", playerMovement.IsFacingEast);
        // animator.SetBool("IsFacingDown", playerMovement.IsFacingSouth);

    }

    void PlayerMovement_OnMoveComplete(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", false);
    }
}
