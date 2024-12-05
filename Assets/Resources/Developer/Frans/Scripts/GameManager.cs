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
    public int m_chooseMiniGame;
    public Scene m_scene;
    public int m_index;

    [SerializeField]
    private List<GameObject> m_ListPlayerUI = new List<GameObject>();
    [SerializeField]
    private List<GameObject> m_ListScoreUI = new List<GameObject>();
    [SerializeField]
    private List<TextMeshProUGUI> m_ListScoreTextUI = new List<TextMeshProUGUI>();
    #region Lists for LeaderBoard
    // (K) Leaderboard Properties
    public List<int> m_playerIDS = new List<int>();
    public List<float> m_scoreList = new List<float>();
    public List<float> m_leaderBoardPositions = new List<float>();
    public int m_playerID;
    public float m_leaderBoardPos;
    public float m_score;
    #endregion

    public bool m_playersSpawned;
    private PlayerMovement[] m_allP_Movement; // (K) Temporary Variable used for counting players.
    public int m_mainCurrentPlayers = 0;
    public float m_maxMiniGamePoints = 100; // (K) This is 1st players reward.

    //PlayerDetector properties
    private int m_playersOutToStopGame;
    private int m_currentLeaderBoardPos = 4;

    public int m_playersOut = 0;
    public int m_playerOutToStopGame;
    public int m_miniGamesPlayed;
    public bool m_isOnePlayerLeft;
    public List<bool> m_outPlayers = new List<bool>();

    public PlayerInputManager m_playerInputManager;


    private void Start()
    {
        m_playerInputManager.onPlayerJoined += CountAmountOfPlayers;
        UIDown();
        m_playersSpawned = false;
    }

    #region Spawning
    public void CountAmountOfPlayers(PlayerInput player)
    {
        //Gets all playermovement scripts
        m_allP_Movement = FindObjectsOfType<PlayerMovement>();

        // (K) Count amount of players in scene.
        m_mainCurrentPlayers = Mathf.Min(4, m_allP_Movement.Length); 

        //Clears the list so we dont have multiple instances of the same player. After wards a new list is generated with the correct amount of players.
        m_playerIDS.Clear();
        m_scoreList.Clear();
        m_leaderBoardPositions.Clear();
        for (int i = 0; i < m_mainCurrentPlayers; i++) // (K) Attach PlayerData to existing players.
        {
            PlayerData addNewPlayerData = new PlayerData();
            m_playerID = addNewPlayerData.playerID;
            m_playerIDS.Add(m_playerID);
            m_score = addNewPlayerData.playerScore;
            m_scoreList.Add(m_score);
            m_leaderBoardPos = addNewPlayerData.playerPosition;
            m_ListScoreUI[i].SetActive(true);
            m_ListPlayerUI[i].SetActive(true);
            /*            PlayerData newPlayerData = new PlayerData
                        {
                            playerID = i,
                            playerScore = 0,
                            playerPosition = 0
                        };*/
            m_playersOutToStopGame = m_mainCurrentPlayers - 1;
            m_currentLeaderBoardPos = m_mainCurrentPlayers;
        }
    }
    #endregion
    #region Repeatable functions
    public void ClearLists()
    {
        //Clears the lists
        m_playerIDS.Clear();
        m_scoreList.Clear();
        m_leaderBoardPositions.Clear();
        foreach(PlayerMovement player in m_allP_Movement)
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
    public void CheckScene()
    {
        m_scene = SceneManager.GetActiveScene();
        m_index = m_scene.buildIndex;
    }
    #endregion
    #region LeaderBoard
    public void GrantPointsToOnePlayer(int playerID2, int leaderBoardPos)
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

/*            m_playerdataList[i].playerPosition = i + 1; // (K) Assign new playerposition
            Debug.Log("Player " + m_playerdataList[i].playerID + " is in "
                + m_playerdataList[i].playerPosition + " with a score of " +
                m_playerdataList[i].playerScore + " points!");*/
        }
    }
    #endregion

    public void PlayerEliminated()
    {
        for(int i = 0; i < m_playerIDS.Count; i++)
        {
            if(m_allP_Movement[i].m_health <= 0)
            {
                m_allP_Movement[i].m_playerOut = true;
                m_allP_Movement[i].PlayerOff();
                GrantPointsToOnePlayer(m_playerIDS[i], m_currentLeaderBoardPos);
                m_currentLeaderBoardPos--;

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
                    Debug.Log(m_allP_Movement[t]);
                    GrantMaxPoints(t);
                    Winner();
                }
            }
        }
    }

    public void Winner()
    {
        for (int i = 0; i < m_playerIDS.Count; i++)
        {
            m_allP_Movement[i].PlayerOn();
        }

        if(m_miniGamesPlayed >= 3)
        {
            UIDown();
            SceneManager.LoadScene("Victory!");
        }
        else
        {
            m_playersSpawned = true;
            SceneManager.LoadScene("PlayerHub");
        }
    }
}