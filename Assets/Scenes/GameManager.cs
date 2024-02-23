using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenLvel(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level2");
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("QuitGame");
    }
}
