using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordspawn : MonoBehaviour
{

    public float smooth;
    public Transform position2;
    
    
    private void FixedUpdate()
    {

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, position2.position, smooth * Time.deltaTime);

        if (gameObject.transform.position == position2.position) gameObject.GetComponent<swordspawn>().enabled = false;
        
        
    }
    
}
