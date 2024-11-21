using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private PlayerMovement m_playerMovement;
    [SerializeField]
    private LayerMask m_layerMask;
    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnDestroy()
    {
        ExplosionDamage(transform.position, 2f);
    }

    private void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, m_layerMask);
        foreach (Collider collider in hitColliders)
        {
            m_playerMovement = collider.gameObject.GetComponent<PlayerMovement>();
            if (m_playerMovement != null)
            {
                m_playerMovement.m_health--;
                if(m_playerMovement.m_health == 0)
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
