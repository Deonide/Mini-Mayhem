using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum GameModes { Combat,Survival,Quick}
public class Portals : MonoBehaviour
{
    //private GameModes m_gameModes;
    private List<GameModes> m_gameModes = new List<GameModes>();


    public int m_AmountOfVotes;

    public PlayerScript[] m_PlayerControls;
    public int playervote = 0;



    private void Start()
    {
        m_PlayerControls = FindObjectsOfType<PlayerScript>();
        playervote = 0;
    }

    private void Update()
    {
       
    } 
  
}
