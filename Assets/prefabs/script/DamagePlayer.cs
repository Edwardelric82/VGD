using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player")
        {

            VitaPersonaggio.instance.DamagePlayer();
        }
    }
}
