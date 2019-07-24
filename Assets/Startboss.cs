using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startboss : MonoBehaviour
{
    public GameObject startboss;

    

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="player")
        {

            startboss.SetActive(true);

            GameManager.Instance.spawn.transform.position = GameObject.FindGameObjectWithTag("RespawnBoss").transform.position;


        }
    }

}
