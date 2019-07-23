using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCharacter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        
       other.transform.parent = gameObject.transform;

        
        
    }

    private void OnTriggerExit(Collider other)
    {
        

        other.transform.parent = GameObject.FindGameObjectWithTag("prefabplayer").transform;
    }
}
