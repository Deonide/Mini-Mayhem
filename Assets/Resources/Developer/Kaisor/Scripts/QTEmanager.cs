using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MiniGames.QuickTimeEvent;
using Unity.VisualScripting;
using UnityEngine;

public class QTEmanager : MonoBehaviour
{
    public PlayerMovement[] p_Movement;
    public LeaderBoardManager boardManager;

    // (K) QTE game properties. 
    private bool isCoroutineRunning = false;

    public float pauseBetweenQTE = 3f;
    public float timeToPress = 2f;
    public float timeIncrement = 1.25f;

    public bool pressWindowActive = false;
    public bool QTE_SequenceActive = false;

    public bool QTE_GameStart = false;
    public int QTE_Cycle = 0;
    public int QTE_Correct_Input;

    public bool ifOnePlayerLeft = false;

    // (K) Player Leaderboard record. (CurrentPlayer, LeaderBoardPosition)
    /* Obsolete
    Dictionary<int, int>
    qte_leaderboard = new Dictionary<int, int>();
    */

    // (K) Player input section.
    //public int currentPlayers = 4; (K) This var should be in a gamemanager at some point

    public int[] playerChosenInput;
    public int[] playerIncorrectAnswers;
    public bool[] playerIsOut;
    public int playersOut = 0;
    private int playersOutToStopGame;
    private int currentLeaderBoardPos;

    void Awake()
    {
        boardManager = FindAnyObjectByType<LeaderBoardManager>();
        for(int i = 0; i < boardManager.mainCurrentPlayers; i++) // (K) find and connect all existing PlayerMov scripts.
        {
            p_Movement[i] = FindAnyObjectByType<PlayerMovement>();
        }

        QTE_SequenceActive = false;
        playersOutToStopGame = boardManager.mainCurrentPlayers - 1;
        currentLeaderBoardPos = boardManager.mainCurrentPlayers;
    }

    void Update()
    {
        if (QTE_GameStart && !isCoroutineRunning)
        {
            isCoroutineRunning = true; // (K) Prevent multiple starts.
            Invoke("StartSequence", 2);
        }
    }

    IEnumerator QTE_SequenceStart()
    {
        while (QTE_SequenceActive)
        {
            pressWindowActive = true; // (K) Start QTE Sequence by opening the button press window and generating the QTE_input.
            GenerateQTE_Input();
            

            yield return new WaitForSeconds(timeToPress); // (K) Wait for the allotted amount of time before closing button press window.

            CheckButtonPressResult();
            QTE_Cycle += 1;
            timeToPress /= timeIncrement; // (K) Increment time to slowly decrease timeToPress.
            pressWindowActive = false;
            

            yield return new WaitForSeconds(pauseBetweenQTE); // (K) Amount of time to pause between each QTE sequence.

            
            pauseBetweenQTE /= timeIncrement; // (K) Increment pause time to slowly decrease timeToPress 

            if (ifOnePlayerLeft)
            {
                QTE_SequenceActive = false;
                yield break; // (K) Insert round end/ victory function here.
            }
        }       
    }

    public void StartSequence()
    {
        StartCoroutine(QTE_SequenceStart());
        QTE_SequenceActive = true;
    }
    public void AddPlayerToLeaderBoard(int currentPlayer, int leaderBoardPos)
    {
        //qte_leaderboard.Add(currentPlayer, leaderBoardPos); Obsolete
        boardManager.GrantPointsToOnePlayer(currentPlayer, leaderBoardPos);
    }
    public void GenerateQTE_Input()
    {
        int randomInput = Random.Range(0, 3);
        switch (randomInput)
        {
            case 0: QTE_Correct_Input = 0; break; // Up Input

            case 1: QTE_Correct_Input = 1; break; // Down Input

            case 2: QTE_Correct_Input = 2; break; // Left Input

            case 3: QTE_Correct_Input = 3; break; // Right Input
        }
    }
    public void CheckButtonPressResult()
    {
        for(int i = 0; i < boardManager.mainCurrentPlayers; i++) // (K) Loop through all current player instances.
        {
            if (playerChosenInput[i] == QTE_Correct_Input && playerIsOut[i] == false) // (K) Check if answer is correct.
            {
                // (K) Player is correct. Continue playing.
                Debug.Log("Correct QTE Input");
            }
            else
            {
                // (K) Player is incorrect. Remove input.
                Debug.Log("Incorrect QTE Input");
                playerIncorrectAnswers[i]++;
                p_Movement[i].TakeDamage();

                if (playerIncorrectAnswers[i] == 4) // (K) amount of mistakes necessary to remove player from game.
                {
                    playerIsOut[i] = true;
                    AddPlayerToLeaderBoard(i, currentLeaderBoardPos);
                    currentLeaderBoardPos--;
                    Debug.Log("Player " + i + " is out!");
                }
            }
            if (playerIsOut[i] == true) // (K) when a player is eliminated add to the playersOut Int.
            {
                playersOut++;
                if (playersOut == playersOutToStopGame) // (K) If three players are out >>
                {
                    ifOnePlayerLeft = true;
                    for (int t = 0; t < boardManager.mainCurrentPlayers; t++) // (K) Check which player won.
                    {
                        if (playerIsOut[t] == false) 
                        {
                            Debug.Log("Player " + t + " wins!");
                        }
                    }
                }
            }
        }
       
        
    }

}

