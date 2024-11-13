using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //character Controler
    private CharacterController m_CharacterController;
    //Movespeed
    [SerializeField] private float m_speed = 5f;

    Vector3 m_move;

    //linking the character controler
    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }
    //connecting the keybinds
    public void OnMovement(InputAction.CallbackContext m_callbackContext)
    {
         m_move=  m_callbackContext.ReadValue<Vector3>();
    }
    //aplying movement
    private void Update()
    {

        m_CharacterController.Move(m_move * m_speed * Time.deltaTime);
    }
}
