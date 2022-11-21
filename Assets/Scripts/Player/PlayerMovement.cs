using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public EventHandler<Vector2> OnMoveStartEvent;
    public EventHandler OnMoveCompleteEvent;

    [SerializeField] float moveSpeed = .2f;
    //[SerializeField] Input playerInput;

    //public Vector2 lastMoveDirection { get; private set; }

    public bool IsMoving { get; private set; } = false;
    public bool IsFacingEast { get; private set; } = false;
    public bool IsFacingSouth { get; private set; } = true;

    void Start()
    {
        BindInputs();
    }

    void BindInputs()
    {
        InputManager.Instance.playerInputActions.Player.Move.performed += OnMove;
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        if (IsMoving)
            return;

        Vector2 inputValue = ctx.ReadValue<Vector2>();

        if (inputValue.y > 0) // Move Up
        {
            OnMoveStart(inputValue);
            IsFacingSouth = false;
            LeanTween.moveY(gameObject, transform.position.y + 1, moveSpeed).setOnComplete(OnMoveComplete);
        }
        else if (inputValue.y < 0) // Move Down
        {
            OnMoveStart(inputValue);
            IsFacingSouth = true;
            LeanTween.moveY(gameObject, transform.position.y - 1, moveSpeed).setOnComplete(OnMoveComplete);
        }
        else if (inputValue.x > 0) // Move Right
        {
            OnMoveStart(inputValue);
            IsFacingEast = true;
            LeanTween.moveX(gameObject, transform.position.x + 1, moveSpeed).setOnComplete(OnMoveComplete);
        }
        else if (inputValue.x < 0) // Move Left
        {
            OnMoveStart(inputValue);
            IsFacingEast = false;
            LeanTween.moveX(gameObject, transform.position.x - 1, moveSpeed).setOnComplete(OnMoveComplete);
        }
    }

    void OnMoveStart(Vector2 inputValue)
    {
        IsMoving = true;
        if (PlayerState.isInOverworld)
            PlayerState.lastKnownOverworldPosition = transform.position;
        //lastMoveDirection = inputValue;

        OnMoveStartEvent?.Invoke(this, inputValue);
    }

    void OnMoveComplete()
    {
        IsMoving = false;

        OnMoveCompleteEvent?.Invoke(this, EventArgs.Empty);
    }
}
