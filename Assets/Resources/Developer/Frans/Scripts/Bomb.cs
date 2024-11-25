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
        //Als de bomb explodeert dan word er een particle effect geinstantiat.
        Instantiate(m_particles, transform.position, Quaternion.identity);

        //Checked of er colliders in de radius zijn en op de juiste layer, 
        //als dat zo is dan haalt het van het gameObject waar die collider opstaat het speler script af en neemt de speler schade.
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
