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

    private bool death = false;

    private bool atk = false;

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

    public float attackDelay = 2f;

    public float Deathanim = 1.5f;

    private swordspawn swordactive;

    public static float attackDelayTimer = 3f;
    

    private void Awake()
    {
        instance = this;

        swordactive = sword.GetComponent<swordspawn>();
    }

    private void FixedUpdate()
    {
        //float distanceToPlayer = Vector3.Distance(PlayerControllerPlatform.instance.transform.position, agent.transform.position);
        
        if (death == true)
        {


            print("stocazzoparte1");

            if (swordactive.enabled == false)
            { swordactive.enabled = true; }

            Deathanim -= Time.deltaTime;
            
            
                
                print("diocane");
                
            if (Deathanim <= 0) animator.SetBool("Death", death);

            if (Deathanim <= -20) GameManager.Instance.GameComplete();

            state = State.Death;
        }
        else
        switch (state)
        {
            case State.Idle:
                StartCoroutine(Routidle());
                break;
                    
            case State.Attack1:
                StartCoroutine(Routatk1());
                break;

            case State.Attack2:
                StartCoroutine(Routatk2());
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

    private void WallFire()
    {
        //Shoot
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);


        //Play Audio
        //bulletAudio.Play();

    }
    

    private void LookAtSlerp(Transform target)
    {
        agent.transform.rotation = Quaternion.Slerp(
            agent.transform.rotation,
            Quaternion.LookRotation(target.transform.position - agent.transform.position),
            Time.deltaTime * rotationSpeed
        );
        agent.transform.rotation = Quaternion.Euler(0f, agent.transform.rotation.eulerAngles.y, 0f);
    }

    public void muori()
    {
        death = true;
    }

    IEnumerator Routatk1()
    {
        LookAtSlerp(PlayerControllerPlatform.instance.transform);
        
        animator.SetTrigger("Atk1");
        
        Fire();

        atk = true;

       // print("atk1");

        yield return new WaitForSeconds(attackDelayTimer);
        
        state = State.Idle;
        
    }

    IEnumerator Routatk2()
    {
        
        animator.SetTrigger("Atk2");
        
        WallFire();

        atk = false;

       // print("atk2");

        yield return new WaitForSeconds(attackDelayTimer);

        state = State.Idle;
            

    }

    IEnumerator Routidle()
    {

        LookAtSlerp(PlayerControllerPlatform.instance.transform);

        animator.SetTrigger("Idle");

        //print("idle");

        yield return new WaitForSeconds(attackDelayTimer+2);

        if (death == true)
            {
                state = State.Death;
            }
        else if (atk == false)
            {
                state = State.Attack1;
            }
        else
        {
            state = State.Attack2;
        }


    }
    

}
