using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Input action Map
    private PlayerInput m_playerInput;
    //Movespeed
    [SerializeField] private float m_speed = 5f;

    //Make a new instance of the PlayerInputs and Enable it
    private void Awake()
    {
        m_playerInput = new PlayerInput();
        m_playerInput.PlayerInputs.Enable();
    }

    //use a vector3 to read the value and link the speed
    private void Update()
    {
        Vector3 m_playerMoveInPut = m_playerInput.PlayerInputs.Movement.ReadValue<Vector3>();

        transform.Translate(m_playerMoveInPut.x * m_speed * Time.deltaTime, 0 ,m_playerMoveInPut.z * m_speed * Time.deltaTime);
    }
}
