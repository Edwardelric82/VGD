using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disablecage : MonoBehaviour
{

    public GameObject cage;

    public GameObject spawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {


            cage.SetActive(false);

            GameManager.Instance.spawn = spawn;
            PlayerControllerPlatform.instance.RespawnPoint = spawn;
        }
    }
}
