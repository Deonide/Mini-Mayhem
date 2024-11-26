using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEmanager : MonoBehaviour
{
    public PlayerMovement p_Movement;

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

    // (K) Player input section (Subject to change).
    public int playerChosenInput;
    public bool playerAnswerCorrect;

    void Awake()
    {
        p_Movement = FindAnyObjectByType<PlayerMovement>();
        QTE_SequenceActive = false;
    }

    void Update()
    {
        if (QTE_GameStart && !isCoroutineRunning)
        {
            isCoroutineRunning = true; // (K) Prevent multiple starts.
            Invoke("StartSequence", 2);
            QTE_SequenceActive = true;
        }
    }

    IEnumerator QTE_SequenceStart()
    {
        while (QTE_SequenceActive)
        {
            pressWindowActive = true; // (K) Start QTE Sequence by opening the button press window.
            GenerateQTE_Input();
            PressIndicatedButton();

            yield return new WaitForSeconds(timeToPress); // (K) Wait for the allotted amount of time before closing button press window.

            QTE_Cycle += 1;
            timeToPress /= timeIncrement; // (K) Increment time to slowly decrease timeToPress.
            pressWindowActive = false;

            yield return new WaitForSeconds(pauseBetweenQTE); // (K) Amount of time to pause between each QTE sequence.

            CheckButtonPressResult();
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
    public void PressIndicatedButton()
    {
        // (K) Use PlayerMovement.cs to determine input and convert to PlayerChosenInput.        
    }

    public void CheckButtonPressResult()
    {
        if (playerChosenInput == QTE_Correct_Input)
        {
            //Player is correct. Continue playing.
        }
        else
        {
            //Player is incorrect. Remove input.
        }
    }

}
