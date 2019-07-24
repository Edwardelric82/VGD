using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour
{
    public GameObject sfera;

    private ParticleSystem part;

    private VitaPersonaggio player;

    public Vector3 scala;

    public int x;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<VitaPersonaggio>();
        part = sfera.GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        

        if(other.tag == "Player" )
        {
            player.DamagePlayer();
            Cresci();
            
        }

        

    }


    void Cresci()
    {
        //part.
        for (x = 0; x <= 1260; x = +1) sfera.transform.localScale += new Vector3(x, x, x);

    }
}
