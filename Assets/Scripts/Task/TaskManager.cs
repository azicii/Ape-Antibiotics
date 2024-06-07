using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.Linq;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [Header("Task UI")]
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private TextMeshProUGUI taskText;
    [SerializeField] private TextMeshProUGUI taskNameText;

    [SerializeField] public List<GameObject> itemsCollected;
    [SerializeField] public int destructiblesGotten;

    //change this to a different object or way of finding tasks
    public List<TaskData> tasksInLevel;

    public TaskData currentTask;

    private static TaskManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Task Manager in the scene");
        }
        instance = this;
    }

    public static TaskManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        taskPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && currentTask != null)
        {
            OpenTask();
        }
    }

    private void FixedUpdate()
    {
        if (currentTask != null)
        {
            TrackTask();
        }
    }

    void TrackTask()
    {
        switch (currentTask.requirement)
        {
            case TaskCompletionRequirements.CollectItems:
                TrackItemsCollected();
                break;
            case TaskCompletionRequirements.KillEnemies:
                TrackEnemyKills();
                break;
            case TaskCompletionRequirements.ReachAnArea:
                TrackPlayerDistanceToArea();
                break;
        }
    }

    public void OpenTask()
    {
        taskPanel.SetActive(!taskPanel.activeInHierarchy);
        taskNameText.text = currentTask.TaskName.ToUpper();
        taskText.text = currentTask.TaskDescription;
    }

    public void FindTask(string taskName)
    {
        currentTask = tasksInLevel.Where(obj => obj.name == taskName).SingleOrDefault();
    }

    void TrackItemsCollected()
    {
        if(DoListsMatch(itemsCollected, currentTask.itemsToCollect))
        {
            Debug.Log("Player collected all items!");
            currentTask.isCompleted = true;
            currentTask = null;
            itemsCollected.Count.Equals(0);
        }
    }

    private bool DoListsMatch(List<GameObject> list1, List<GameObject> list2)
    {
        return list1.TrueForAll(list2.Contains) && list2.TrueForAll(list1.Contains);
    }

    void TrackEnemyKills()
    {
        Debug.Log("Killing enemies!");
    }

    void TrackPlayerDistanceToArea()
    {
        //Debug.Log("Go to Area!");
        if (currentTask.areaToReach.GetComponent<ReachPoint>().playerInReachPoint == true)
        {
            Debug.Log("Player reached area!");
            currentTask = null;
        }
    }
}



