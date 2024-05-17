using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Task UI")]
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private TextMeshProUGUI taskText;

    private bool taskIsPlaying;
    private bool taskIsDone = false;

    private const string TASK_TAG = "task";

    private static QuestManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static QuestManager GetInstance()
    {
        return instance;
    }

    private void Update()
    {
        if (!taskIsPlaying)
        {
            return;
        }

        //change to character interact, or get rid of if it becomes irrelevant
        /*if (canContinueToNextLine == true && Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Continuing Story");
            ContinueStory();
        }*/

        if (taskIsDone == true)
        {
            //ContinueStory();
        }
    }
    private IEnumerator DisplayLine(string line)
    {
        taskText.text = "";
        taskIsDone = false;

        //if text has items, you can hide them here

        foreach (char letter in line.ToCharArray())
        {
            /*if (Input.GetKey(KeyCode.I)) //change to character interact, or get rid of if it becomes irrelevant
            {
                dialogueText.text = line;
                break;
            }*/

            taskText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        //if text has items, you can unhide them here

        taskIsDone = true;
    }
}
