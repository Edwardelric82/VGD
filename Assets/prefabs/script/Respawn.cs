using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Transform respawnPoint;

    private Animator ciao;



    void OnTriggerEnter(Collider other)
    {
        ciao = player.GetComponent<Animator>();

        ciao.SetTrigger("death");

        GameManager.Instance.Respawn();

        player.transform.position = respawnPoint.transform.position;

        ciao.SetTrigger("Idle");

        

    }

}