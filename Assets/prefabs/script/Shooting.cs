using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    float bulletSpeed = 1100;
    public GameObject bullet;

    //AudioSource bulletAudio;

    // Use this for initialization
    void Start()
    {

        //bulletAudio = GetComponent<AudioSource>();

    }

    void Fire()
    {
        //Shoot
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        Destroy(tempBullet, 0.5f);

        //Play Audio
        //bulletAudio.Play();

    }


    // Update is called once per frame
    void Update()
    {

       

    }
}
