using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCompleteUi : MonoBehaviour
{

    public Text Score;

    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {

        Score.text = "Score : "+ GameManager.Instance.score.ToString() +""; 

        highScore.text = "High Score : "+ GameManager.Instance.highScore.ToString() +"";
        
    }
    
    public void RestartLevel()
    {
        
        GameManager.Instance.Reset();

    }

    public void NextLevel()
    {

        GameManager.Instance.IncreaseLevel();

    }

    public void menuGame()
    {

        SceneManager.LoadScene("Home");

    }
    
}
