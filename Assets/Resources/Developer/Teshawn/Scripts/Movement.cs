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
    public Vector2 m_move;


    private Collider m_collider;

    //linking the character controler
    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }
    
    //aplying movement
    private void Update()
    {
        Debug.Log(m_move);
        //adding gravity
        Vector3 m_moveVector = Vector3.zero;
        if(m_CharacterController.isGrounded == false)
        {
            m_moveVector += Physics.gravity;
        }
        
        //moving and gravity       
        Vector3 m_move2 = new Vector3(m_move.x , 0, m_move.y); // (K) Convert Read value Vector2 to Vector3

        m_CharacterController.Move(m_moveVector * Time.deltaTime);
        m_CharacterController.Move(m_move2 * m_speed * Time.deltaTime);
    }   


}
