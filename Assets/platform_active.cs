using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform_active : MonoBehaviour
{
    public GameObject platform1;
    public GameObject platform2;
    public GameObject platform3;

    private int nkey=1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (nkey)
            {
                case 1:
                    platform1.GetComponent<PlatformDamage>().keyc();//centro")
                    nkey++;
                    break;

                case 2:
                    platform2.GetComponent<PlatformDamage>().keyc();//centro")
                    nkey++;
                    break;


                case 3:
                    platform3.GetComponent<PlatformDamage>().keyc();//centro")

                    break;



        
            }
        }
          

    }
}
