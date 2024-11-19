using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum GameModes { Combat,Survival,Quick}
public class Portals : MonoBehaviour
{
    public GameModes m_gameModes;

    public int m_AmountOfVotes;

    public PlayerMovement[] m_PlayerControls;
    public int playervote = 0;



    private void Start()
    {
        m_PlayerControls = FindObjectsOfType<PlayerMovement>();
        playervote = 0;
    }

    private void Update()
    {
        if (m_gameModes == GameModes.Combat)
        {

        }

        if (m_gameModes == GameModes.Survival)
        {

        }

        if (m_gameModes == GameModes.Quick)
        {

        }
    } 
  
}
