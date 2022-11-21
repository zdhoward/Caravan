using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayerMask;

    PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        BindInputs();

        if (PlayerState.isInOverworld && PlayerState.lastKnownOverworldPosition != null)
            transform.position = PlayerState.lastKnownOverworldPosition;
    }

    void BindInputs()
    {
        InputManager.Instance.playerInputActions.Player.Interact.performed += OnInteract;
    }

    void OnInteract(InputAction.CallbackContext ctx)
    {
        Vector2 directionNorthSouth = playerMovement.IsFacingSouth ? Vector2.down : Vector2.up;
        Vector2 directionEastWest = playerMovement.IsFacingEast ? Vector2.right : Vector2.left;

        // Check Up/Down
        //Debug.DrawLine(transform.position, transform.position + (Vector3)directionNorthSouth, Color.red, 3f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionNorthSouth, 1f, interactableLayerMask);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact();
                return;
            }
        }

        // Check Left/Right
        //Debug.DrawLine(transform.position, transform.position + (Vector3)directionEastWest, Color.red, 3f);
        hit = Physics2D.Raycast(transform.position, directionEastWest, 1f, interactableLayerMask);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact();
                return;
            }
        }
    }

    void RayCastDirection()
    {

    }
}
