using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject taskManager;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            taskManager.GetComponent<TaskManager>().itemsCollected.Add(this.gameObject);
            Debug.Log("Item collected!");
            //Destroy(this.gameObject);
        }
    }
}
