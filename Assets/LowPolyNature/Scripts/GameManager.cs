using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    public int score = 0;

    public int highScore = 0;

    public int currentLevel = 1;

    public int highestLevel = 2;

    

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Dont destroy on reloading the scene
        DontDestroyOnLoad(gameObject);

        
    }

    public void IncreaseScore(int amount)
    {

        score += amount;

        print("New Score: " + score.ToString());

        if(score > highScore)
        {
            highScore = score;

            print("New high score: " + highScore);
        }

    }

    public void Reset()
    {
        score = 0;

        currentLevel = 1;

        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void IncreaseLevel()
    {
        if (currentLevel < highestLevel)
        {
            currentLevel++;
        }
        else
        {
            currentLevel = 1;
        }

        SceneManager.LoadScene("Level" + currentLevel);

    }

    public void GameComplete()
    {

        SceneManager.LoadScene("GameComplete");
    }

    public PlayerController Player;
    
}
