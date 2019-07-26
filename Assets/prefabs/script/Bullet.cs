using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    
    private GameObject player;
    

    public float timer = 0;

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);

        


    }

    private void Update()
    {
        float disToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * disToMove);//instead of vector player.pos
        if (timer > 2.0f) gameObject.GetComponent<MeshRenderer>().enabled = true;

        timer += 1.0f * Time.deltaTime;
        if(timer >20)
        {
            Destroy(gameObject);

        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<VitaPersonaggio>().DamagePlayer();
            Destroy(gameObject);
        }

        
    }

}
