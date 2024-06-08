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

    [SerializeField] public List<string> itemsCollected;
    [SerializeField] public List<string> eliminatedEnemies;
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
        for (int i = 0; i < currentTask.requirements.Length; i++)
        {
            switch (currentTask.requirements[i])
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
    }

    public void OpenTask()
    {
        taskPanel.SetActive(!taskPanel.activeInHierarchy);
        taskNameText.text = currentTask.taskName.ToUpper();
        taskText.text = "";
        for(int i = 0; i < currentTask.requirements.Length; i++)
        {
            if(currentTask.requirements[i] == TaskCompletionRequirements.CollectItems)
            {
                string collection = "";
                for (int j = 0; j < currentTask.itemsToCollect.Count; j++)
                {
                    if(j != currentTask.itemsToCollect.Count - 1)
                    {
                        collection += "a " + currentTask.itemsToCollect[j] + ", ";
                    } else
                    {
                        collection += "and a " + currentTask.itemsToCollect[j];
                    }
                }

                taskText.text += " - Collect "+ collection + "<br>";

                collection = "";
            } else if(currentTask.requirements[i] == TaskCompletionRequirements.KillEnemies)
            {
                string hitList = "";
                for (int j = 0; j < currentTask.enemiesToKill.Count; j++)
                {
                    if (j != currentTask.enemiesToKill.Count - 1)
                    {
                        hitList += "a " + currentTask.enemiesToKill[j] + ", ";
                    }
                    else
                    {
                        hitList += "and a " + currentTask.enemiesToKill[j];
                    }
                }

                taskText.text += " - Eliminate " + hitList + "<br>";

                hitList = "";
            }
            else if(currentTask.requirements[i] == TaskCompletionRequirements.ReachAnArea)
            {
                taskText.text += " - Reach " + currentTask.areaToReach.name + "<br>";
            }
            else
            {
                Debug.LogWarning("Invalid completion requirement! Please add requirement to TaskCompletionRequirements");
            }
        }
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
        }
    }

    private bool DoListsMatch(List<string> list1, List<string> list2)
    {
        list1.Sort();
        list2.Sort();

        if(list1.Count != list2.Count)
        {
            return false;
        }

        return true;
    }

    void TrackEnemyKills()
    {
        if (DoListsMatch(eliminatedEnemies, currentTask.enemiesToKill))
        {
            Debug.Log("Player eliminated all enemies!");
        }
    }

    void TrackPlayerDistanceToArea()
    {
        if (currentTask.areaToReach.GetComponent<ReachPoint>().playerInReachPoint == true)
        {
            Debug.Log("Player reached area!");
        }
    }
}



