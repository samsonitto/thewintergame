using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Objectives : MonoBehaviour
{
    // variables
    public GameObject[] gameObjectives;
    private GameObject player;
    private GameObject itemManager;
    public Text nextObjective;
    public Text nextObjectiveInfo;
    public Text distanceToTheNextObject;
    private int objIndex;
    private GameObject currentObjective;
    private GameObject spitBody;

    void Start()
    {
        objIndex = 0;
        player = GameObject.FindWithTag("Player");
        itemManager = GameObject.FindWithTag("ItemManager");
        spitBody = GameObject.FindWithTag("AirplaneBody");
        currentObjective = gameObjectives[objIndex];
        StartCoroutine(ShowNextObjective());
    }

    // Update is called once per frame
    void Update()
    {
        ShowDistance();
    }

    private IEnumerator ObjectiveCompleted()
    {
        nextObjective.text = "Objective " + (objIndex + 1) + " \"" + currentObjective.GetComponent<Item>().partName + "\" is completed";
        yield return new WaitForSeconds(5);
        if(objIndex < gameObjectives.Length - 1)
        {
            objIndex++;
            currentObjective = gameObjectives[objIndex];
            ShowObjectiveInfo();
            StartCoroutine(ShowNextObjective());
        }
        else
        {
            currentObjective = spitBody;
            nextObjective.text = "You have all the parts now,\nhead back to the airplane and fix it \nby clicking \"Airplane\" in the crafting menu";
            nextObjectiveInfo.text = "Last objective: Fix the airplane by clicking \"Airplane\" in the crafting menu";
            yield return new WaitForSeconds(5);
            nextObjective.text = "";
        }
    }

    private IEnumerator ShowNextObjective()
    {
        nextObjective.text = "Your next objective is to collect " + currentObjective.GetComponent<Item>().partName + "! \nFollow the distance to the next objective!\n(Press [Tab] to see your next objective)";
        ShowObjectiveInfo();
        SpawnItem();
        yield return new WaitForSeconds(5);
        nextObjective.text = "";
    }

    public void CheckForObjective(GameObject obj)
    {
        if(obj.GetComponent<Item>().partName == currentObjective.GetComponent<Item>().partName)
        {
            StartCoroutine(ObjectiveCompleted());
        }
    }

    public void ShowObjectiveInfo()
    {
        nextObjectiveInfo.text = "Objective " + (objIndex + 1) + ": Collect " + currentObjective.GetComponent<Item>().partName + ". (Follow the distance)";
    }

    public void ShowDistance()
    {
        float distance = Vector3.Distance(player.transform.position, currentObjective.transform.position);

        distanceToTheNextObject.text = "Distance: " + distance;
    }

    public void SpawnItem()
    {
        currentObjective.SetActive(true);
    }
}
