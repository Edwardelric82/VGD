using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudmanager : MonoBehaviour
{
    public static hudmanager instance;

    public Image blackScreen;

    public RawImage []  hearts;

    public Text scoreLabel;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {

        scoreLabel.text = "Score: " + GameManager.Instance.score;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ImageRespawnOn()
    {
        blackScreen.gameObject.SetActive(true);
    }

    public void ImageRespawnOff()
    {
        blackScreen.gameObject.SetActive(false);
    }

    public void life(int vite)
    {
        /*
        for(int i = 1;i<=3;i++)
        {
            if(i<=vite)
            hearts[i].gameObject.SetActive(true);
            else
            {
                hearts[i].gameObject.SetActive(false);
            }

        }
        */
        
        switch (vite)
        {
            case 1:
                {
                    hearts[0].gameObject.SetActive(true);
                    hearts[1].gameObject.SetActive(false);
                    hearts[2].gameObject.SetActive(false);


                }
                break;


            case 2:
                {
                    hearts[0].gameObject.SetActive(true);
                    hearts[1].gameObject.SetActive(true);
                    hearts[2].gameObject.SetActive(false);


                }
                break;


            case 3:
                {
                    hearts[0].gameObject.SetActive(true);
                    hearts[1].gameObject.SetActive(true);
                    hearts[2].gameObject.SetActive(true);

                }
                break;

        }
        
    }
}