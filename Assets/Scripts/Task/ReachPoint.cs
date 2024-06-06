using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachPoint : MonoBehaviour
{
    public bool playerInReachPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInReachPoint = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInReachPoint = false;
        }
    }
}
