using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    #region Private Members

    private Animator _animator;

    private CharacterController _characterController;

    private float Gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;

    private InventoryItemBase mCurrentItem = null;

    private HealthBar mHealthBar;

    private int startHealth;

    private Rigidbody rb;

    private int count;

    private float timer = 4f;

    #endregion

    #region Public Members

    public float Speed = 5.0f;

    public float RotationSpeed = 240.0f;

    public Inventory Inventory;

    public GameObject Hand;

    

    public hudmanager hud;

    public float JumpSpeed = 10.0f;

    public Text countText;

    public AudioSource birrasuono;

    #endregion

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        hud.Refresh();


        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        
        mHealthBar.Min = 0;
        mHealthBar.Max = Health;
        startHealth = Health;
        mHealthBar.SetValue(Health);

 
        
    }

    

    #region Health

    [Tooltip("Amount of health")]
    public int Health = 100;

    public bool IsDead
    {
        get
        {
            return Health == 0 ;
        }
    }
  

    public void Rehab(int amount)
    {
        Health += amount;
        if (Health > startHealth)
        {
            Health = startHealth;
        }

        mHealthBar.SetValue(Health);
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
            Health = 0;

        mHealthBar.SetValue(Health);

        if (IsDead)
        {
            _animator.SetTrigger("death");
        }

    }

    #endregion


    void FixedUpdate()
    {
        if (!IsDead)
        {
            
        }
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
        if (!IsDead && mIsControlEnabled)
        {
            // Interact with the item
            if (mInteractItem != null && Input.GetKeyDown(KeyCode.F))
            {
                // Interact animation
                mInteractItem.OnInteractAnimation(_animator);
            }

            // Execute action with item
            if (mCurrentItem != null && Input.GetMouseButtonDown(0))
            {
                // Dont execute click if mouse pointer is over uGUI element
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    // TODO: Logic which action to execute has to come from the particular item
                    _animator.SetTrigger("attack_1");
                }
            }

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
                    _moveDirection.y = JumpSpeed ;

                }
                else
                {
                    _animator.SetBool("is_in_air", false);
                    _animator.SetBool("run", move.magnitude > 0);
                }
            }


            _moveDirection.y -= Gravity;


            _characterController.Move(_moveDirection * Time.deltaTime);
        }
    }

    public bool isattack;

    private InteractableItemBase mInteractItem = null;

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
        else if (other.gameObject.tag == "levelup")
        {

            GameManager.Instance.LevelComplete();


        }
        else if (other.gameObject.tag == "death" || other.gameObject.tag == "Enemy")
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

