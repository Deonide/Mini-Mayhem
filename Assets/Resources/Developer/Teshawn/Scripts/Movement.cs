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
    //movement
    private Vector3 m_move;


    private Collider m_collider;

    //linking the character controler
    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }
    //connecting the keybinds for movement
    public void OnMovement(InputAction.CallbackContext m_callbackContext)
    {
         m_move=  m_callbackContext.ReadValue<Vector3>();
    }
    //aplying movement
    private void Update()
    {
        //adding gravity
        Vector3 m_moveVector = Vector3.zero;
        if(m_CharacterController.isGrounded == false)
        {
            m_moveVector += Physics.gravity;
        }
        //moving and gravity
        m_CharacterController.Move(m_moveVector * Time.deltaTime);
        m_CharacterController.Move(m_move * m_speed * Time.deltaTime);
    }   


}
