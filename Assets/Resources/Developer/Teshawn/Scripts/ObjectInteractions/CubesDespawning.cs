using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesDespawning : MonoBehaviour
{
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage();
        }
       Destroy(this.gameObject);
    }
}
