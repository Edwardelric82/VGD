using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platboss : MonoBehaviour
{
    public static platboss instance;

    public Transform movingPlatform;

    public Transform position2;
    public Vector3 newPosition;

    public GameObject gabbia;
    public GameObject boss;

    private float timgabbia=2.0f;
    private bool mov = false;

    public float smooth;



    private void Awake()
    {
        instance = this;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (mov == true)
        {

            movingPlatform.position = Vector3.Lerp(movingPlatform.position, newPosition, smooth * Time.deltaTime);

            if (movingPlatform.position == newPosition)
            {
                mov = false;
                
                gabbia.SetActive(false);
            }
        }
        

        
            
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            newPosition = position2.position;
            mov = true;
            gabbia.SetActive(true);
            other.transform.parent = gameObject.transform;

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = GameObject.FindGameObjectWithTag("prefabplayer").transform;

            boss.SetActive(true);

            Destroy(GameObject.FindGameObjectWithTag("platboss"));

        }
    }
    
}
