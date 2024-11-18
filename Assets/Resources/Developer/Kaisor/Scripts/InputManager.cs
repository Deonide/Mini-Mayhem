using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // (K) Player Movement Script reference
    public Movement p_movement;
    // (K) Input Actions Asset
    public PlayerInput m_PlayerInput;

    void Awake()
    {
        // (K) Instatiating an input actions asset
        m_PlayerInput = new PlayerInput();
        p_movement = GetComponent<Movement>();
        m_PlayerInput.Enable();
    }

    // (K) Enabling the action map and inputs
    private void OnEnable()
    {
        m_PlayerInput.Enable();
        m_PlayerInput.PlayerInputs.Movement.performed += OnPlayerMovement;
        m_PlayerInput.PlayerInputs.Movement.canceled += OnPlayerMovement;
        m_PlayerInput.PlayerInputs.Interact.performed += OnPlayerInteract;
    }

    private void OnDisable()
    {
        m_PlayerInput.PlayerInputs.Movement.performed -= OnPlayerMovement;
        m_PlayerInput.PlayerInputs.Movement.canceled -= OnPlayerMovement;
        m_PlayerInput.PlayerInputs.Interact.performed -= OnPlayerInteract;
        m_PlayerInput.Disable();
    }

    private void OnPlayerMovement(InputAction.CallbackContext context)
    {
        p_movement.m_move = context.ReadValue<Vector2>();
    }

    private void OnPlayerInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact input received!");
        // (K) Add a reference to the script that contains the function for interact and place that function here.
    }

}
