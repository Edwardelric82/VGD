using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUiManager : MonoBehaviour
{

    public Text Score;

    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {

        Score.text = "Score : "+ GameManager.Instance.score.ToString() +""; 

        highScore.text = "High Score : "+ GameManager.Instance.highScore.ToString() +"";
        
    }
    
    public void RestartGame()
    {

        GameManager.Instance.Reset();

    }

    public void menuGame()
    {
        SceneManager.LoadScene("Home");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Home");
    }
}
