using System.Collections;
using UnityEngine;

public class PlatformDamage : MonoBehaviour {

    public static PlatformDamage instance; 
    public Transform movingPlatform;
    
    public Transform position2;
    public Vector3 newPosition;

    private bool mov = false;
    private bool key = false;

    public float smooth;

    public GameObject spada;

    

    private void Awake()
    {
        instance = this;
    }



    // Update is called once per frame
    void FixedUpdate ()
    { 
        if(mov == true)
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
            print("Muoviti Cazzo");

        }


    }

    public void keyc()
    {
        key = true;

    }
    
    
}