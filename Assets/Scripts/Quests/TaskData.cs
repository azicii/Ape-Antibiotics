using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task Data", menuName = "ScriptableObjects/Task Data", order = 1)]
public class TaskData : ScriptableObject
{
    public string TaskName;

    public string TaskDescription;
}
