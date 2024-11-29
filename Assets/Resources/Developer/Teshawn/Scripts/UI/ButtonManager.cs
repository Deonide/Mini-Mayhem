using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.GetSceneByName("PlayerHub");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
