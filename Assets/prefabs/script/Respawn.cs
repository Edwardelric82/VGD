using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
               
       if (other.tag=="Player")
        {

            VitaPersonaggio.instance.DamagePlayer();
            
            GameManager.Instance.Respawn();

        }
       
    }

}