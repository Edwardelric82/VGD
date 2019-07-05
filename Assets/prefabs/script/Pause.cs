using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    
     [SerializeField] private GameObject pausePanel;

     [SerializeField] private bool isPaused;


    public Text Score;

    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {

        Score.text = "Score : " + GameManager.Instance.score + "";

        highScore.text = "High Score : " + GameManager.Instance.highScore + "";

    }
   
    void Update()

    {
        if (Input.GetButtonDown("Cancel"))
        {

            isPaused = !isPaused;
            
        }

        if (isPaused)
        {
            PauseGame();
        }
        else 
        {
            ContinueGame();
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        GameManager.Instance.Reset();

    }

    public void menuGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");

    }

    public void PauseGame()

    {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            Cursor.visible = true;
        //Disable scripts that still work while timescale is set to 0

    }

    public void ContinueGame()

    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Cursor.visible = false;
        isPaused = false;
        //enable the scripts again

    }
     
}
