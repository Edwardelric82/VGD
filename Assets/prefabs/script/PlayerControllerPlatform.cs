using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllerPlatform : MonoBehaviour
{
    public static PlayerControllerPlatform instance;

    public CharacterController character;
    public GameObject model;
    public GameObject[] modelParts;

    public Animator animator;
    public new Camera camera;

    

    public float timer = 10.0f;

    public float stepback = 10.0f;

    public bool key = false;

    public hudmanager hud;

    public Text countText;

    public AudioSource birrasuono;

    public AudioSource winesuono;

    public AudioSource keyCollected;

    public AudioSource powerUp;

    public AudioSource powerDown;


    public GameObject sfera;
    

    public GameObject RespawnPoint;

    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpHeight = 15f;
    public float gravityScale = 5f;
    public float rotateSpeed = 5f;
    public float bounceHeight = 5f;

    public bool isKnockedBack = false;
    public bool isKnockedBackDef = false;


    public float knockbackPower = 3f;
    public float knockbackPowerDef = 10f;

    public float knockbackDuration = 0.5f;
    private float knockbackTimer;

    private GameObject env;

    private bool grounded;
    private Vector3 motion;

    //--------------------------------------------
    public static float powerTimeOut = 5f;

    public static float bossfight = 1f;
    private bool powerUpActive = false;
    //--------------------------------------------

    private void Awake()
    {
        
        // Dont destroy on reloading the scene
        

        instance = this;
        

        // Dont destroy on reloading the scene
        

        grounded = character.isGrounded;

        //GameManager.Instance.RoutineStart();
    }


    private void Update()
    {
        if (grounded)
            motion.y = 0;

        if (isKnockedBack)
            CalculateMotionKnockback();
        else if (isKnockedBackDef)
            CalculateMotionKnockbackDef();
        else
            CalculateMotion();

        motion.y += gravityScale * Physics.gravity.y * Time.deltaTime;

        character.Move(motion * Time.deltaTime);

        grounded = character.isGrounded;

        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(motion.x) + Mathf.Abs(motion.z));
    }

    private void CalculateMotion()
    {
        float currentHeight = motion.y;
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        motion = transform.forward * verticalInput + transform.right * horizontalInput;
        motion.Normalize();
        motion *= Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        motion.y = currentHeight;

        if (grounded && Input.GetButtonDown("Jump"))
        {
            //AudioManager.instance.PlayEffect(AudioManager.SFX.Jump);
            motion.y = jumpHeight;
        }

        if (horizontalInput != 0 || verticalInput != 0)
        {
            transform.rotation = Quaternion.Euler(0f, camera.transform.rotation.eulerAngles.y, 0f);

            model.transform.rotation = Quaternion.Slerp(
                model.transform.rotation,
                Quaternion.LookRotation(new Vector3(motion.x, 0f, motion.z)), 
                rotateSpeed * Time.deltaTime
            );
        }
    }

    private void CalculateMotionKnockback()
    {
        knockbackTimer -= Time.deltaTime;
        isKnockedBack = knockbackTimer > 0;

        motion = character.transform.forward * -knockbackPower;
    }

    private void CalculateMotionKnockbackDef()
    {
        knockbackTimer -= Time.deltaTime;
        isKnockedBack = knockbackTimer > 0;

    }

    public void Knockback()
    {
        isKnockedBack = true;
        knockbackTimer = knockbackDuration;
    }

    public void KnockbackDef()
    {
        isKnockedBackDef = true;
        knockbackTimer = knockbackDuration *2;
    }

    public void Bounce()
    {
        motion.y = bounceHeight;
    }

    public void Debounce(float height)
    {
        motion.y = height;
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "beer")
        {
            print("Birra presa");

            GameManager.Instance.IncreaseScore(1);

            hud.Refresh();

            birrasuono.Play();

            VitaPersonaggio.instance.HealPlayer();

            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "wine")
        {

            print("vino preso");

            GameManager.Instance.IncreaseScore(2);

            hud.Refresh();

            winesuono.Play();

            VitaPersonaggio.instance.HealPlayer();

            Destroy(other.gameObject);


        }
        else if (other.gameObject.tag == "levelup")
        {

            if (key == true)
            {
                if (GameManager.Instance.currentLevel == 3)
                {
                    GameManager.Instance.GameComplete();

                }
                else
                {
                    GameManager.Instance.LevelComplete();

                }



            }


        }
        else if (other.gameObject.tag == "key")
        {
            print("Key Collected");

            key = true;

            keyCollected.Play();

            sfera.SetActive(true);

            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "goldkey")
        {
            print("Gold Key Collected");

            

            other.gameObject.SetActive(false);
            //Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "death")
        {

            VitaPersonaggio.instance.DamagePlayer();

            GameManager.Instance.Respawn();

            animator.SetTrigger("Death");



            timer -= Time.deltaTime;

            if (timer == 0)
            {
                SceneManager.LoadScene("Game Over");
            }

        }
        else if (other.gameObject.tag == "Enemy")
        {

            VitaPersonaggio.instance.DamagePlayer();

            Knockback();

            CalculateMotionKnockback();

            VitaPersonaggio.instance.HudLife();




        }//-----------------------------PowerUp---------------------------------
        else if (other.gameObject.tag == "PowerJump" && powerUpActive == false)
        {
            powerUpActive = true;
            powerUp.Play();
            Destroy(other.gameObject);
            StartCoroutine(CountDownJump());

        }
        else if (other.gameObject.tag == "PowerSpeed" && powerUpActive == false)
        {
            powerUpActive = true;
            powerUp.Play();
            Destroy(other.gameObject);
            StartCoroutine(CountDownSpeed());

        }
        else if (other.gameObject.tag == "PowerDown" && powerUpActive == false)
        {
            powerUpActive = true;
            powerDown.Play();
            Destroy(other.gameObject);
            StartCoroutine(CountDownDown());

        }
        else if (other.gameObject.tag == "elimina")
        {
            env = GameObject.FindGameObjectWithTag("prova");
            env.SetActive(false);


        }
        /*else if (other.gameObject.tag == "vita" && VitaPersonaggio.instance.health < 3)
        {
            Destroy(other.gameObject);
            VitaPersonaggio.instance.HealPlayer();
        }*/


    }

    IEnumerator CountDownJump()
    {
        jumpHeight += 10f;
        yield return new WaitForSeconds(powerTimeOut+3);
        jumpHeight -= 10f;
        powerUpActive = false;
    }
    IEnumerator CountDownSpeed() {

        runSpeed += 10f;
        yield return new WaitForSeconds(powerTimeOut+5);
        runSpeed -= 10f;
        powerUpActive = false;
    }
    IEnumerator CountDownDown()
    {

        jumpHeight -= 10f;
        runSpeed -= 10f;
        moveSpeed -= 5f;
        yield return new WaitForSeconds(powerTimeOut);
        jumpHeight += 10f;
        runSpeed += 10f;
        moveSpeed += 5f;
        powerUpActive = false;
    }
    


}
