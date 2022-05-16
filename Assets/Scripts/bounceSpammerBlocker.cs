using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceSpammerBlocker : MonoBehaviour
{

    [SerializeField]private LayerMask ground;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag =="Player")
        {
            other.gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }
    

}
