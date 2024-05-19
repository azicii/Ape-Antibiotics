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

    //change this to a different object or way of finding tasks
    public List<TaskData> tasksInLevel;

    public TaskData currentTask;

    private static TaskManager instance;

    Transform player;
    float distanceLeeway = 2f;

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
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        Debug.Log("Collecting Items!");

        // on completion: currentTask.isCompleted = true;
    }

    void TrackEnemyKills()
    {
        Debug.Log("Killing enemies!");
    }

    void TrackPlayerDistanceToArea()
    {
        Debug.Log("Go to Area!");
    }
}


