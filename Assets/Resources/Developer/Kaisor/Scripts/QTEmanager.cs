using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEmanager : MonoBehaviour
{
    public PlayerMovement p_Movement;

    public float pauseBetweenQTE = 2f;
    public float timeToPress = 2f;
    public float timeIncrement = 1.25f;

    public bool pressWindowActive = false;
    public bool QTE_SequenceActive = false;

    public bool QTE_GameStart = false;
    public int QTE_Cycle = 0;
    public int QTE_Correct_Input;

    public bool ifOnePlayerLeft = false;

    // (K) Player input section (Subject to change)
    public int playerChosenInput;
    public bool playerAnswerCorrect;

    void Awake()
    {
        p_Movement = FindAnyObjectByType<PlayerMovement>();
        QTE_SequenceActive = false;
    }

    void Update()
    {
        Invoke("QTE_SequenceStart", 2);
    }

    IEnumerator QTE_SequenceStart()
    {
        QTE_SequenceActive = true;
        pressWindowActive = true; // (K) Start QTE Sequence by opening the button press window.
        GenerateQTE_Input();
        PressIndicatedButton();

        yield return new WaitForSeconds(timeToPress); // (K) Wait for the allotted amount of time before closing button press window.

        QTE_Cycle += 1;
        timeToPress /= timeIncrement; // (K) Increment time to slowly decrease timeToPress.
        pressWindowActive = false;

        yield return new WaitForSeconds(pauseBetweenQTE); // (K) Amount of time to pause between each QTE sequence.

        QTE_SequenceActive = false;
        if (ifOnePlayerLeft)
        {
            yield return null; // (K) Insert round end/ victory function here.
        }
        else
        {
            yield return StartCoroutine(QTE_SequenceStart()); // (K) Restart Sequence if there's more than one player left.
        }
    }

    public void GenerateQTE_Input()
    {
        int randomInput = Random.Range(0, 3);
        switch (randomInput)
        {
            case 0: QTE_Correct_Input = 0; break;

            case 1: QTE_Correct_Input = 1; break;

            case 2: QTE_Correct_Input = 2; break;

            case 3: QTE_Correct_Input = 3; break;
        }
    }
    public void PressIndicatedButton()
    {
        //Use PlayerMovement.cs to determine input and convert to PlayerChosenInput.
        if (playerChosenInput == QTE_Correct_Input)
        {
            //Player is correct. Continue playing.
        }
        else
        {
            //Player is incorrect. Remove input.
        }
    }

    public void CheckButtonPressResult()
    {

    }

}
