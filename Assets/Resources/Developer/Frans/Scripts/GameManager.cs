using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Lists for LeaderBoard
    // (K) Leaderboard Properties
    [SerializeField]
    private List<GameObject> m_ListPlayerUI = new List<GameObject>();
    [SerializeField]
    private List<GameObject> m_ListScoreUI = new List<GameObject>();

    public List<int> m_playerIDS = new List<int>();
    public int m_playerIDForList;
    #endregion
    #region Links
    //PlayerDetector properties
    private PlayerMovement[] m_allP_Movement; // (K) Temporary Variable used for counting players.
    public PlayerInputManager m_playerInputManager;
    #endregion
    #region SceneChecker
    public int m_index;
    public Scene m_scene;
    #endregion
    #region Decide Winner
    public List<bool> m_outPlayers = new List<bool>();
    public bool m_isOnePlayerLeft;

    public int m_playersOut = 0;
    public int m_mainCurrentPlayers = 0;
    public int m_playerOutToStopGame;
    public int m_winningPlayer;
    private int m_playersOutToStopGame;
    #endregion


    public bool m_playersSpawned;
    [SerializeField]
    private Canvas m_ScoreCanvas;
    private void Start()
    {
        m_playerInputManager.onPlayerJoined += CountAmountOfPlayers;

        UIDown();
        m_playersSpawned = false;
    }

    #region Spawning
    private class SortIntDescending : IComparer<int>
    {
        //Checks if int a is bigger or smaller then int b to order the specified list.
        int IComparer<int>.Compare(int a, int b) //implement Compare
        {
            if (a > b)
                return -1; //normally greater than = 1
            if (a < b)
                return 1; // normally smaller than = -1
            else
                return 0; // equal
        }
    }

    public void CountAmountOfPlayers(PlayerInput player)
    {
        //Gets all playermovement scripts
        m_allP_Movement = FindObjectsOfType<PlayerMovement>();

        // (K) Count amount of players in scene.
        m_mainCurrentPlayers = Mathf.Min(4, m_allP_Movement.Length); 

        //Clears the list so we dont have multiple instances of the same player. After wards a new list is generated with the correct amount of players.
        m_playerIDS.Clear();

        for (int i = 0; i < m_allP_Movement.Length; i++) // (K) Attach PlayerData to existing players.
        {
            m_playerIDForList = m_allP_Movement[i].m_playerID;
            m_playerIDS.Add(m_playerIDForList);
            m_allP_Movement[i].m_playerID++;

            m_playerIDS.Sort(new SortIntDescending());

            m_ListPlayerUI[i].SetActive(true);
            m_playersOutToStopGame = m_mainCurrentPlayers - 1;
        }

    }
    #endregion
    #region Repeatable functions
    public void ClearLists()
    {
        //Clears the lists
        m_playerIDS.Clear();

        foreach (PlayerMovement player in m_allP_Movement)
        {
            Destroy(player.gameObject);
        }
    }

    public void UIDown()
    {
        foreach (GameObject PlayerUI in m_ListPlayerUI)
        {
            PlayerUI.SetActive(false);
        }
        foreach (GameObject UIScore in m_ListScoreUI)
        {
            UIScore.SetActive(false);
        }
    }
    public void DestroyUI()
    {
        Destroy(m_ScoreCanvas);
    }

    public void CheckScene()
    {
        m_scene = SceneManager.GetActiveScene();
        m_index = m_scene.buildIndex;
    }

    #endregion
    #region UnUsed
    /*    public void GrantPointsToOnePlayer(int playerID2, int leaderBoardPos)
        {
            // (K) lowest points are the amount the last place player gets. Each player after gets double the points.
            float tempPoints = m_maxMiniGamePoints /= leaderBoardPos;
            for (int i = 0; i < m_playerIDS.Count; i++)
            {
                if (m_playerIDS[i] == playerID2)
                {
                    m_scoreList[i] += tempPoints;
                    Debug.Log(m_scoreList[i]);
                    m_ListScoreTextUI[i].text = m_scoreList[i].ToString();
                }
            }
        }

        public void GrantMaxPoints(int playerID2)
        {
            // (K) lowest points are the amount the last place player gets. Each player after gets double the points.
            float tempPoints = m_maxMiniGamePoints;
            for (int i = 0; i < m_playerIDS.Count; i++)
            {
                if (m_playerIDS[i] == playerID2)
                {
                    m_scoreList[i] += tempPoints * 2;
                    Debug.Log(m_scoreList[i]);
                    m_ListScoreTextUI[i].text = m_scoreList[i].ToString();
                }
            }

            // (K) Reorder list based on new scores.
            m_leaderBoardPositions = m_scoreList.OrderByDescending(score => score).ToList();
        }

        public void CheckCurrentLeaderBoardStatus()
        {
            for (int i = 0; i < m_scoreList.Count; i++)
            {

    *//*            m_playerdataList[i].playerPosition = i + 1; // (K) Assign new playerposition
                Debug.Log("Player " + m_playerdataList[i].playerID + " is in "
                    + m_playerdataList[i].playerPosition + " with a score of " +
                    m_playerdataList[i].playerScore + " points!");*//*
            }
        }

        }*/
    #endregion
    #region Decide Winner
    public void PlayerEliminated()
    {
        for (int i = 0; i < m_playerIDS.Count; i++)
        {
            if (m_allP_Movement[i].m_health <= 0)
            {
                m_allP_Movement[i].m_playerOut = true;
                m_allP_Movement[i].PlayerOff();

                if (m_allP_Movement[i].m_playerOut == true) // (K) when a player is eliminated add to the playersOut Int.
                {
                    m_playersOut++;
                    if (m_playersOut >= m_playersOutToStopGame) // (K) If three players are out >>
                    {
                        m_isOnePlayerLeft = true;
                    }
                }
            }
        }

        if (m_isOnePlayerLeft)
        {
            for (int t = 0; t < m_mainCurrentPlayers; t++) // (K) Check which player won.
            {
                if (m_allP_Movement[t].m_playerOut == false)
                {
                    Debug.Log(m_playerIDS[t]);
                    m_winningPlayer = m_playerIDS[t];
                    Winner();
                }
            }
        }
    }

    public void Winner()
    {
        for (int i = 0; i < m_playerIDS.Count; i++)
        {
            m_allP_Movement[i].PlayerOff();
        }

        UIDown();
        m_playerInputManager.onPlayerJoined -= CountAmountOfPlayers;
        SceneManager.LoadScene("Victory!");
    }
    #endregion
}