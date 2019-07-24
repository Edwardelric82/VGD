using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCharacter1: MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

       
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = GameObject.FindGameObjectWithTag("prefabplayer").transform;
    }
}
