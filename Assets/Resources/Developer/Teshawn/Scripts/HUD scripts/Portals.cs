using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public enum GameModes { Combat,Survival,Quick}
public class Portals : MonoBehaviour
{
    public GameModes m_gameModes;

    private void Update()
    {
        if(m_gameModes == GameModes.Combat)
        {
            Debug.Log("combat");
        }
        if (m_gameModes == GameModes.Survival)
        {
            Debug.Log("Survival");
        }
        if (m_gameModes == GameModes.Quick)
        {
            Debug.Log("QTE");
        }
    }
}
