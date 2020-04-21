using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{
    // variables
    public GameObject[] gameObjectives;
    private GameObject player;
    private GameObject itemManager;
    public Text nextObjective;
    private int objIndex;
    private GameObject currentObjective;

    void Start()
    {
        objIndex = 0;
        player = GameObject.FindWithTag("Player");
        itemManager = GameObject.FindWithTag("ItemManager");
        currentObjective = gameObjectives[objIndex];
        StartCoroutine(ShowNextObjective());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ObjectiveCompleted()
    {
        nextObjective.text = "Objective " + (objIndex + 1) + " \"" + currentObjective.GetComponent<Item>().partName + "\" is completed";
        yield return new WaitForSeconds(5);
        if(objIndex < gameObjectives.Length - 1)
        {
            objIndex++;
            currentObjective = gameObjectives[objIndex];
            StartCoroutine(ShowNextObjective());
        }
    }

    private IEnumerator ShowNextObjective()
    {
        nextObjective.text = "Your next objective is to collect " + currentObjective.GetComponent<Item>().partName + "! \nFollow the distance to the next objective!\n(Press [Tab] to see your next objective)";
        yield return new WaitForSeconds(10);
        nextObjective.text = "";
    }

    public void CheckForObjective(GameObject obj)
    {
        if(obj == currentObjective)
        {
            StartCoroutine(ObjectiveCompleted());
        }
    }

    public void ShowObjectiveInfo()
    {
        nextObjective.text = "Objective " + objIndex + 1 + ": Collect " + currentObjective.GetComponent<Item>().partName + ". (Follow the distance)";
    }
}
