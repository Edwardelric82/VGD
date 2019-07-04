using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public string LevelToLoad;
    private float timer = 10f;
    private Texture timerSeconds;
    
    // Use this for initialization
    void Start ()
    {
        
    }

    void delay(string livello)
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {

        }


    }

    // Update is called once per frame
    void Update ()
    { timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            
        }
    }
}