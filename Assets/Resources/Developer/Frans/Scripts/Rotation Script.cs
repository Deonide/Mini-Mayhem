using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    private Transform tr;
    private float rotationSpeed = 20f;
    private Vector3 rotationAxis = new Vector3(0, 1, 0);


    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        tr.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
