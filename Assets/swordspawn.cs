using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordspawn : MonoBehaviour
{
    public static swordspawn instance;

    public float speed;

    private bool death;

    private void Start()
    {
        instance = this;
        
    }

    private void FixedUpdate()
    {
        if (death == true)
        {
            float disToMove = speed * Time.deltaTime;
            transform.Translate(Vector3.down * disToMove);//instead of vector player.pos

        }
        

        
    }


   
   

   
}
