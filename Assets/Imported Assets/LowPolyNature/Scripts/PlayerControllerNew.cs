using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControllerNew : MonoBehaviour
{
    private Animator _animator;

    private CharacterController _characterController;

    private Rigidbody rb;

    private Vector3 _moveDirection = Vector3.zero;

    private float Gravity = 20.0f;

    //private float timer = 4;

    private int Lifes = 3;

    public Image lifebar;


    public float timer = 10.0f;

    public float Speed = 10.0f;

    public float RotationSpeed = 240.0f;

    public bool key = false;

    public hudmanager hud;

    public float JumpSpeed = 10.0f;

    public Text countText;

    public AudioSource birrasuono;

    public AudioSource winesuono;

    public GameObject sfera;

    public GameObject RespawnPoint;


    

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();

        _animator = GetComponent<Animator>();

        _characterController = GetComponent<CharacterController>();


    }

    private bool mIsControlEnabled = true;

    public void EnableControl()
    {
        mIsControlEnabled = true;
    }

    public void DisableControl()
    {
        mIsControlEnabled = false;
    }

 

    // Update is called once per frame
    void Update()
    {

       

        if (mIsControlEnabled)
        {
          
            // Get Input for axis
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");


            // Calculate the forward vector
            Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

            if (move.magnitude > 1f) move.Normalize();

            // Calculate the rotation for the player
            move = transform.InverseTransformDirection(move);

            // Get Euler angles
            float turnAmount = Mathf.Atan2(move.x, move.z);

            transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);


            {
                _moveDirection = transform.forward * move.magnitude;

                _moveDirection *= Speed;

                if (Input.GetButton("Jump"))
                {
                    _animator.SetBool("is_in_air", true);
                    _moveDirection.y = JumpSpeed;

                }
                else
                {
                    _animator.SetBool("is_in_air", false);
                    _animator.SetBool("run", move.magnitude > 0);
                }
            }


            _moveDirection.y -= Gravity;


            _characterController.Move(_moveDirection * Time.deltaTime*2);
        }





    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "beer")
        {
            print("Birra presa");

            GameManager.Instance.IncreaseScore(1);

            hud.Refresh();

            birrasuono.Play();

            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "wine")
        {

            print("vino preso");

            GameManager.Instance.IncreaseScore(2);

            hud.Refresh();

            winesuono.Play();

            Destroy(other.gameObject);


        }
        else if (other.gameObject.tag == "levelup")
        {

            if (key == true)
            {
                GameManager.Instance.LevelComplete();

            }


        }
        else if (other.gameObject.tag == "key")
        {
            print("Key Collected");

            key = true;

            sfera.SetActive(true);

            Destroy(other.gameObject);

        }
        else if (other.gameObject.tag == "death")
        {

            print("game over");

            _animator.SetTrigger("death");

            DisableControl();

            timer -= Time.deltaTime;

            if (timer == 0)
            {
                SceneManager.LoadScene("Game Over");
            }

        }
        else if (other.gameObject.tag == "Enemy")
        {

            print("game over");

            _animator.SetTrigger("death");

            DisableControl();

            timer -= Time.deltaTime;

            if (timer == 0)
            {
                SceneManager.LoadScene("Game Over");
            }



        }
        else if (other.gameObject.tag == "PowerJump")
        {
            print("power up preso");



            Destroy(other.gameObject);

        }
        


    }



}
