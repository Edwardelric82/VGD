using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUiManager : MonoBehaviour
{
    public GameObject menu;

    public GameObject tutorial;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Tutorial()
    {
        menu.SetActive(false);
        tutorial.SetActive(true);
        


    }

} 