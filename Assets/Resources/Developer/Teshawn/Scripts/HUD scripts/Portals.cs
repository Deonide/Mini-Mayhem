using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum GameModes { Combat,Survival,Quick}
public class Portals : MonoBehaviour
{
    public GameModes m_gameModes;
    
    public int m_AmountOfVotes = 0;

    private void Start()
    {
        m_AmountOfVotes = 0;
    }
}
