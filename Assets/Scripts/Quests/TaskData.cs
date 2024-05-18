using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task Data", menuName = "ScriptableObjects/Task Data", order = 1)]
public class TaskData : ScriptableObject
{
    [Header("Task Text")]
    public string TaskName;
    public string TaskDescription;

    [Header("Clear Condition")]
    public bool killEnemies;
    public bool reachArea;
    public bool survive;

    bool isCompleted;

    public bool IsCompleted()
    { 
        Debug.Log(this.name + " is completed!");
        
        if(isCompleted)
        {
            return true;
        }

        return false;
    }
}
