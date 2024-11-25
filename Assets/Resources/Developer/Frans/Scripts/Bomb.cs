using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_layerMask;

    [SerializeField]
    private GameObject m_particles;

    private PlayerMovement m_playerMovement;
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
        Instantiate(m_particles, transform.position, Quaternion.identity);
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, m_layerMask);
        foreach (Collider collider in hitColliders)
        {
            m_playerMovement = collider.gameObject.GetComponent<PlayerMovement>();
            if (m_playerMovement != null)
            {
                m_playerMovement.TakeDamage();
            }
        }
    }
}
