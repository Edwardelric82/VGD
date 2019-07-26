using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCharacter1: MonoBehaviour
{
    public GameObject gabbia;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {

            
            gabbia.SetActive(true);

            platboss.instance.movement();

            other.transform.parent = gameObject.transform;


        }

    }

    private void OnTriggerExit(Collider other)
    {
        


        //if (other.tag == "player") other.transform.parent = GameObject.FindGameObjectWithTag("prefabplayer").transform;


    }
}
