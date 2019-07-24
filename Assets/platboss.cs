using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platboss : MonoBehaviour
{
    public static platboss instance;

    public Transform movingPlatform;

    public Transform position2;
    public Vector3 newPosition;
    
    public GameObject boss;
    
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

            
        }
        

        
            
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = gameObject.transform;

        }
        
    }

    public void movement()
    {
        newPosition = position2.position;
        
        mov = true;
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
