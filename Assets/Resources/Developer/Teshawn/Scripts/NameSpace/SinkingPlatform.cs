using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGames;
using MiniGames.SinkingPlatforms;

public class SinkingPlatform : MonoBehaviour
{

    private Vector3 m_originalSPawnPoint;
    private GameObject m_SinkingObject;
    private Rigidbody m_Rigidbody;
    private float m_dropCoolDown;
    private float m_MaxCD = 5f;
    public bool G_isFalling;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_SinkingObject = this.gameObject;
        m_originalSPawnPoint = this.transform.position;
        m_dropCoolDown = m_MaxCD;
    }

    private void Update()
    {
        SinkingPlatforms.Sinking(m_SinkingObject, m_originalSPawnPoint, G_isFalling, m_dropCoolDown, m_MaxCD);
        if (G_isFalling) 
        {

            m_Rigidbody.useGravity = true;

        }
        else
        {
            m_Rigidbody.useGravity = false;
        }
        
    }
}
