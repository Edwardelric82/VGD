using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUiManager : MonoBehaviour
{

    public Text Score;

    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {

        Score.text = GameManager.Instance.score.ToString();

        highScore.text = GameManager.Instance.score.ToString();
        
    }
    
    public void RestartGame()
    {

        GameManager.Instance.Reset();

    }
    
}
