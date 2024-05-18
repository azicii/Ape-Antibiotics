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

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
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
}
