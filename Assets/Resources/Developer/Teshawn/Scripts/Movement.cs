using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;


public class Movement : MonoBehaviour
{
    //Movespeed
    [SerializeField] private float m_speed = 5f;

    //Rigidbody
    private Rigidbody m_rb;
    
    private PlayerInput m_playerInput;

    private Vector2 m_data;

    private void Awake()
    {
        m_playerInput = new PlayerInput();
/*        m_playerInput.KeyboardControls;*/
    }

/*    public void OnMove(InputAction.CallbackContext m_callbackContext)
    {

    }*/

    private void Update()
    {
        if (m_rb != null)
        {
            m_data = m_playerInput.KeyboardControls.Movement.ReadValue<Vector2>();
            m_rb.velocity = new Vector3(m_data.x * m_speed * Time.deltaTime , 0, m_data.y * m_speed * Time.deltaTime);
        }
    }
}
