using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    string name;

    private void Start()
    {
        name = SceneManager.GetActiveScene().name;

        if (name == "Loading Screen")
        {
            StartCoroutine(WaitTime());
        }
    }


    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1.5f);

        LoadNextLevel();
    }


    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void LoadPreviousLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }


    public void LoadWinScreen()
    {
        SceneManager.LoadScene("Win Screen");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
