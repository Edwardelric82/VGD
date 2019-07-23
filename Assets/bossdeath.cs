using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossdeath : MonoBehaviour
{
    public static bossdeath instance;

    public float speed;

    private bool death=false;

    public Animator anim;

    private void Start()
    {
        instance = this;
        death = true;

    }

    private void FixedUpdate()
    {
        if (death == true)
        {
            float disToMove = speed * Time.deltaTime;
            transform.Translate(Vector3.down * disToMove);//instead of vector player.pos
            anim.SetBool("Death",death);
        }



    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
