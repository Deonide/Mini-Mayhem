using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadUI : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);   
    }
}