using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEscape : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hero"))
        {
            // player escape.

            // scene transition
        }
    }

}
