using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene("PlayerHub");
    }

    public void GoToMainMenu()
    {
        GameManager.Instance.ClearLists();
        Destroy(GameManager.Instance);
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
