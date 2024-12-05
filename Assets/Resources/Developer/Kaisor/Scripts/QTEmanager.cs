using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEmanager : MonoBehaviour
{
    public PlayerMovement[] p_Movement;


    // (K) QTE game properties. 
    private bool isCoroutineRunning = false;

    #region Timer 
    public float pauseBetweenQTE = 3f;
    public float timeToPress = 2f;
    public float timeIncrement = 1.25f;
    #endregion
    #region Bools
    public bool pressWindowActive = false;
    public bool QTE_SequenceActive = false;
    public bool QTE_GameStart = false;
    #endregion

    public int QTE_Cycle = 0;
    public int QTE_Correct_Input;


    public int playersOut = 0;

    public int[] playerChosenInput;
    public int[] playerIncorrectAnswers;
    public bool[] playerIsOut;



    void Awake()
    {
        PlayerMovement[] allP_Movement = FindObjectsOfType<PlayerMovement>();

        p_Movement = new PlayerMovement[GameManager.Instance.m_mainCurrentPlayers];
        for (int i = 0; i < GameManager.Instance.m_mainCurrentPlayers; i++) // (K) find and connect all existing PlayerMov scripts.
        {
            p_Movement[i] = allP_Movement[i];
        }

        // (K) Make sure the index's match the amount of players.
        playerChosenInput = new int[GameManager.Instance.m_mainCurrentPlayers];
        playerIncorrectAnswers = new int[GameManager.Instance.m_mainCurrentPlayers];
        playerIsOut = new bool[GameManager.Instance.m_mainCurrentPlayers];
    }

    #region Coroutine
    void Update()
    {
        if (QTE_GameStart && !isCoroutineRunning)
        {
            isCoroutineRunning = true; // (K) Prevent multiple starts.
            Invoke("StartSequence", 2);
        }
    }

    public void StartSequence()
    {
        QTE_SequenceActive = true;
        StartCoroutine(QTE_SequenceStart());
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

            if (GameManager.Instance.m_isOnePlayerLeft)
            {
                QTE_SequenceActive = false;
                yield break; ; // (K) Insert round end/ victory function here.
            }
        }
    }
    #endregion
     
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
        for(int i = 0; i < GameManager.Instance.m_mainCurrentPlayers; i++) // (K) Loop through all current player instances.
        {
            if (playerChosenInput[i] == QTE_Correct_Input && p_Movement[i].m_playerOut == false) // (K) Check if answer is correct.
            {
                // (K) Player is correct. Continue playing.
                // (K) Place jumping animation for player here! (grab the p_Movement[i] and play the animation from there.)
                Debug.Log("Correct QTE Input");
            }
            else
            {
                // (K) Player is incorrect. Remove input.
                Debug.Log("Incorrect QTE Input");
                p_Movement[i].TakeDamage();
            }
        }
    }
}

