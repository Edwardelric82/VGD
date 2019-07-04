using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudmanager : MonoBehaviour
{
    public static hudmanager instance;

    public Image blackScreen;

    public Image[] hearts;

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

}