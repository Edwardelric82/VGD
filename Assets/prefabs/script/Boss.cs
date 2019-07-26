using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public static Boss instance;
    
    public NavMeshAgent agent;
    public Animator animator;

    public float bulletSpeed = 1100;
    public GameObject bullet;
    public GameObject sword;
    public GameObject Startboss;
    public GameObject sphere;

    public bool death = false;

    public bool atk = false;

    public bool shot = true;

    public int walln = 1;

    public int ncolpi =0;

    public int vita = 3; 

    //AudioSource bulletAudio;

    public enum State
    {
        Idle,
        Attack1,
        Attack2,
        Death
    };
    public State state;

    public float rotationSpeed = 3f;

    public float Delay =0;
    public float IdleDelay = 2.5f;
    public float attackDelay1 = 2.0f;
    public float attackDelay2 = 4.0f;
    
    public float Deathanim = 1.5f;

    private swordspawn swordactive;

    public static float attackDelayTimer = 2f;
    

    private void Awake()
    {
        instance = this;

        swordactive = sword.GetComponent<swordspawn>();
    }

    private void FixedUpdate()
    {

        LookAtSlerp(PlayerControllerPlatform.instance.transform);

        if (death == true)
        {

            swordactive.enabled = true; 

            Deathanim -= Time.deltaTime;
            
            animator.SetBool("Death", death);

            if (Deathanim <= -10)
            {
                
                Startboss.SetActive(false);

                sphere.SetActive(true);

            }
            state = State.Death;
        }
        else
        switch (state)
        {
            case State.Idle:
                    {

                        

                        animator.SetBool("Idle",true);

                        Delay -= Time.deltaTime;

                        if (death == true)
                        {
                            state = State.Death;
                        }
                        else if(Delay >= 0)
                        {
                            state = State.Idle;
                        }
                        else if (Delay <= 0 && atk == false && ncolpi <3)
                        {
                            shot = true;
                            state = State.Attack1;
                            ncolpi++;
                            if (ncolpi == 3) atk = true;
                            animator.SetBool("Idle", false);
                        }
                        else if (Delay <= 0 && atk == true)
                        {
                            shot = true;
                            state = State.Attack2;
                            animator.SetBool("Idle", false);
                            ncolpi = 0;
                        }
                        
                        //animator.SetBool("Idle", false);
                        
                    }
                    break;
                    
            case State.Attack1:
                    {
                        Delay -= Time.deltaTime;

                        animator.SetBool("Atk1", true);

                        if (Delay <=0 && shot == true)
                        {
                            
                            Fire();
                            Delay = attackDelay1;

                            
                            shot = false;
                        }
                        else if (Delay <=0 && shot ==false)
                        {
                            state = State.Idle;
                            animator.SetBool("Atk1", false);
                            Delay = IdleDelay;
                        }
                        
                        
                        
                        

                    }
                    break;

            case State.Attack2:
                    {

                        Delay -= Time.deltaTime;

                        animator.SetBool("Atk2", true);

                        if (Delay <= 0 && shot == true)
                        {
                            if(walln<=8)
                            {
                                Fire();
                                walln++;
                                Delay = attackDelay2;
                            }
                            else
                            {

                               
                                atk = false;
                                shot = false;
                                walln = 1;
                            }

                        }
                        else if (Delay <= 0 && shot == false)
                        {

                            state = State.Idle;
                            animator.SetBool("Atk2", false);

                            Delay = IdleDelay;
                        }

                    }
                    break;
                    

        }


    }

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            VitaPersonaggio.instance.DamagePlayer();
            PlayerControllerPlatform.instance.Knockback();
            
        }
    }

   

    
    private void Fire()
    {
        //Shoot
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);


        //Play Audio
        //bulletAudio.Play();

    }
    /*
    private void WallFire() uc
    {
        //Shoot
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);


        //Play Audio
        //bulletAudio.Play();

    }
    */

    private void LookAtSlerp(Transform target)
    {
        agent.transform.rotation = Quaternion.Slerp(
            agent.transform.rotation,
            Quaternion.LookRotation(target.transform.position - agent.transform.position),
            Time.deltaTime * rotationSpeed
        );
        agent.transform.rotation = Quaternion.Euler(0f, agent.transform.rotation.eulerAngles.y, 0f);
    }

    public void ferisci()
    {
        vita--;

        if(vita==0) death = true;
    }
    
}
