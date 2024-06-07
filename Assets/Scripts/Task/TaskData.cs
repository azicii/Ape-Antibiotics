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

    [SerializeField] public bool isCompleted;

    [SerializeField] /*[HideInInspector]*/ public List<GameObject> itemsToCollect;
    [SerializeField] [HideInInspector] public List<GameObject> enemiesToKill;
    [SerializeField] [HideInInspector] public GameObject areaToReach;
}

[CustomEditor(typeof(TaskData))]
public class TaskDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var taskData = (TaskData)target;

        base.OnInspectorGUI();

        var itemList = serializedObject.FindProperty("itemsToCollect");
        var hitList = serializedObject.FindProperty("enemiesToKill");
        var area = serializedObject.FindProperty("areaToReach");

        switch (taskData.requirement)
        {
            case TaskCompletionRequirements.CollectItems:
               // EditorGUILayout.PropertyField(itemList, new GUIContent("Items to Collect"), true);
                break;
            case TaskCompletionRequirements.KillEnemies:
                EditorGUILayout.PropertyField(hitList, new GUIContent("Enemies to Kill"), true);
                break;
            case TaskCompletionRequirements.ReachAnArea:
                EditorGUILayout.PropertyField(area, new GUIContent("Area to Reach"), true);
                break;
            default:
                return;
        }
    }
}
