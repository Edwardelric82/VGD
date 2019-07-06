using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    private PlayerControllerPlatform controller;

    public int score = 0;

    public int currentLevel = 1;

    public int highestLevel = 3;

    public int highScore = 0 ;

    private GameObject player ;



    public GameObject spawn;

    

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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerPlatform>();

        spawn = GameObject.FindGameObjectWithTag("respawn");

        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        

        StartCoroutine(StartRoutine());
    }


    private IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(1);

        PlayerControllerPlatform.instance.enabled = false;

        //AudioManager.instance.PlayEffect(AudioManager.SFX.Pipe);

        Vector3 start = PlayerControllerPlatform.instance.transform.position;
        Vector3 end = PlayerControllerPlatform.instance.transform.position + new Vector3(0f, 1.5f);

        float time = 0f;
        while (time <= 1f)
        {
            time += Time.deltaTime;
            PlayerControllerPlatform.instance.transform.position = Vector3.Lerp(start, end, time);
            yield return new WaitForEndOfFrame();
        }

        //spawn.transform.position = player.transform.position;

        PlayerControllerPlatform.instance.enabled = true;
        CameraController.instance.brain.enabled = true;
    }

    public void Respawn()
    {
        /*foreach (var platform in FindObjectsOfType<Platform>())
            platform.playerOnPlatform = false;*/

        StartCoroutine(RespawnRoutine());
    }

    public IEnumerator RespawnRoutine()
    {


        
        player.SetActive(false);

        
        hudmanager.instance.ImageRespawnOn();

        player.transform.position = PlayerControllerPlatform.instance.RespawnPoint.transform.position;
   

        yield return new WaitForSeconds(2f);

        hudmanager.instance.ImageRespawnOff();
        //HealthManager.instance.ResetHealth();



        //PlayerController.instance.isKnockedBack = false;
        player.SetActive(true);
        
        
        
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

        SceneManager.LoadScene("Level" + currentLevel);

        GameManager.Instance.Start();

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

        Reset();

    }

    public void LevelComplete()
    {

        SceneManager.LoadScene("Level Complete");
    }

    public void GameComplete()
    {

        SceneManager.LoadScene("GameComplete");
    }

    public void RoutineStart()
    {

        StartCoroutine(StartRoutine());

    }
    
    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}
