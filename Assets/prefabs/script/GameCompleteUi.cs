using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCompleteUi : MonoBehaviour
{

    public Text Score;

    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        Score.text = "Score : "+ GameManager.Instance.score.ToString() +""; 

        highScore.text = "High Score : "+ GameManager.Instance.highScore.ToString() +"";
        
    }
    
    public void RestartLevel()
    {
        
        GameManager.Instance.Reset();

    }

    public void NextLevel()
    {
        GameManager.Instance.score = 0;
        GameManager.Instance.IncreaseLevel();
        GameManager.Instance.Respawn();
    }

    public void menuGame()
    {

        GameManager.Instance.score = 0;
        GameManager.Instance.currentLevel = 1;

        SceneManager.LoadScene("Home");
        

    }
    
}
