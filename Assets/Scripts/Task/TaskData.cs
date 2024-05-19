using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Task Data", menuName = "Create Task/Task Data", order = 1)]
public class TaskData : ScriptableObject
{
    [Header("Task Text")]
    public string TaskName;
    [Multiline]
    public string TaskDescription;

    [Header("Completion Requirement")]
    public TaskCompletionRequirements requirement;

    [Header("Requirement Values")]
    [SerializeField] [HideInInspector] public int itemsToCollect;
    [SerializeField] [HideInInspector] public int enemiesToKill;
    [SerializeField] [HideInInspector] public Transform areaToReach;
    [SerializeField] public bool isCompleted;
}

[CustomEditor(typeof(TaskData))]
public class TaskDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var taskData = (TaskData)target;

        base.OnInspectorGUI();

        switch(taskData.requirement)
        {
            case TaskCompletionRequirements.CollectItems:
                taskData.itemsToCollect = EditorGUILayout.IntField("Items to collect: ", taskData.itemsToCollect);
                break;
            case TaskCompletionRequirements.KillEnemies:
                taskData.enemiesToKill = EditorGUILayout.IntField("Enemies to kill: ", taskData.enemiesToKill);
                break;
            case TaskCompletionRequirements.ReachAnArea:
                taskData.areaToReach = EditorGUILayout.ObjectField("Area to reach: ", taskData.areaToReach, typeof(Transform), true) as Transform;
                break;
            default:
                return;
        }
    }
}
