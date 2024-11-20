using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Voting : MonoBehaviour
{
    [SerializeField] private List<Portals> m_ports = new List<Portals>();
    [SerializeField] private int m_drawResult;
    [SerializeField] private int m_additionalVote;
    private void Start()
    {
        m_ports = FindObjectsOfType<Portals>().ToList();
        Debug.Log(m_ports[0].m_gameModes);

    }
    private void Update()
    {
        m_ports = m_ports.OrderByDescending(gamemode => gamemode.m_AmountOfVotes).ToList();

        if (m_ports[0].m_AmountOfVotes == m_ports[1].m_AmountOfVotes)
        {
            m_drawResult =  Random.Range(0, 2);
            if (m_drawResult == 0)
            {
                CheckingGameMode();
            }else if (m_drawResult == 1)
            {
                m_ports[1].m_AmountOfVotes += m_additionalVote;
                CheckingGameMode();
            }
        }
        else
        {
            CheckingGameMode();
        }
    }


    public void CheckingGameMode()
    {
        switch (m_ports[0].m_gameModes)
        {
            case GameModes.Survival:
                PlayingSurvival();
                break;
            case GameModes.Combat:
                PlayingCombat();
                break;
            case GameModes.Quick:
                PlayingQuick();
                break;
        }
    }
    public void PlayingSurvival()
    {
       
    }
    public void PlayingCombat()
    {
        
    }
    public void PlayingQuick()
    {

    }
}
