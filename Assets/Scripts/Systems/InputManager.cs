using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    PlayerInput playerInput;
    public PlayerInputActions playerInputActions;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one InputManager in this scene.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
    }

    void Start()
    {
        playerInputActions.Player.Enable();
    }

    public void EnableDialogcontrols()
    {
        playerInputActions.Player.Disable();
        playerInputActions.Dialog.Enable();
    }

    public void DisableDialogcontrols()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Dialog.Disable();
    }
}
