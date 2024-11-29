using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Voting : MonoBehaviour
{
    [SerializeField] 
    private List<Portals> m_ports = new List<Portals>();
    [SerializeField] 
    private int m_drawResult;
    [SerializeField] 
    private int m_additionalVote;
    [SerializeField]
    private bool m_hasVoted;
    private float m_voteTimer = 5f;

    public int g_totalVotes;

    private void Start()
    {
        m_ports = FindObjectsOfType<Portals>().ToList();
    }

    private void Update()
    {
       if(g_totalVotes >= 1)
       {
            m_hasVoted = true;
            m_voteTimer -= Time.deltaTime;
       }
        if (m_hasVoted && m_voteTimer <= 0)
        {
            m_voteTimer = 0;
            m_ports = m_ports.OrderByDescending(gamemode => gamemode.m_AmountOfVotes).ToList();

            if (m_ports[0].m_AmountOfVotes == m_ports[1].m_AmountOfVotes)
            {
                m_drawResult = Random.Range(0, 2);
                if (m_drawResult == 0)
                {
                    CheckingGameMode();
                }
                else if (m_drawResult == 1)
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
        SceneManager.LoadScene("Survival Mini Games");
    }

    public void PlayingCombat()
    {
        SceneManager.LoadScene("Combat Mini Games");
    }

    public void PlayingQuick()
    {
        SceneManager.LoadScene("QTE Mini Games");
    }
}
