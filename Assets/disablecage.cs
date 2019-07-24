using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disablecage : MonoBehaviour
{

    public GameObject cage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") cage.SetActive(false); 
    }
}
