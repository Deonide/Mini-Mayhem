using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesDespawning : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
