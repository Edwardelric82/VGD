using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudmanager : MonoBehaviour
{



    public Text scoreLabel;

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
}
