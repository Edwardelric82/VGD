using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    
    private GameObject player;
    

    public float timer;

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);

        


    }

    private void Update()
    {
        float disToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * disToMove);

        timer += 1.0f * Time.deltaTime;
        if(timer >20)
        {
            Destroy(gameObject);

        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            player.GetComponent<VitaPersonaggio>().DamagePlayer();
            Destroy(gameObject);
        }

        
    }

}
