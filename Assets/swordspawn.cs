using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordspawn : MonoBehaviour
{
    public static swordspawn instance;

    public float smooth;

    public float timer;

    public Transform position2;
    public Vector3 newPosition;

    private bool death;

    private bool stop;

    private void Start()
    {
        instance = this;
        death = true;
        stop = false;
        newPosition = position2.position;
    }

    private void FixedUpdate()
    {
        if (death == true && stop == false)
        {

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPosition, smooth * Time.deltaTime);


            timer += 1.0f * Time.deltaTime;
            if (timer > 20)
            {
                stop = true;

            }
        }

        


    }

    
}
