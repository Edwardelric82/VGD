using System.Collections;
using UnityEngine;

public class PlatformDamage1 : MonoBehaviour {

    public static PlatformDamage1 instance; 
    public Transform movingPlatform;
    
    public Transform position2;
    public Vector3 newPosition;

    public GameObject goldkey;

    private bool mov = false;
    private bool key = false;

    public float smooth;

    

    private void Awake()
    {
        instance = this;
        print("awake plat");
    }



    // Update is called once per frame
    void FixedUpdate ()
    { 
        if(mov == true && movingPlatform.position != newPosition)
        {

            movingPlatform.position = Vector3.Lerp(movingPlatform.position, newPosition, smooth * Time.deltaTime);
           
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && key == true )
        {
            newPosition = position2.position;
            mov = true;
            Boss.instance.ferisci();
            key = false;

            goldkey.SetActive(true);

        }else if (other.tag == "Player")
        {
            //"Sei senza Chiave!"

        }


    }

    private void OnTriggerExit(Collider other)
    {

    }


    public void keyc()
    {
        key = true;
        print("kec");
    }
    
    
}