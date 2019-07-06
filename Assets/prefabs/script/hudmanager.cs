using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudmanager : MonoBehaviour
{
    public static hudmanager instance;

    public Image blackScreen;

    public RawImage[]  hearts;

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
        for(int i = 0;i<=3;i++)
        {
            if(i<vite+1)
            hearts[i].gameObject.SetActive(true);
            else
            {
                hearts[i].gameObject.SetActive(false);
            }

        }
        
    }
}