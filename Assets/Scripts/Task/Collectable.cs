using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject taskManager;
    public string itemName;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            taskManager.GetComponent<TaskManager>().itemsCollected.Add(itemName);
            Debug.Log("Item collected!");
            //Destroy(this.gameObject);
        }
    }
}
