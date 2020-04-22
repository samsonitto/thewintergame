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
    }

    private IEnumerator ShowNextObjective()
    {
        nextObjective.text = "Your next objective is to collect " + currentObjective.GetComponent<Item>().partName + "! \nFollow the distance to the next objective!\n(Press [Tab] to see your next objective)";
        ShowObjectiveInfo();
        yield return new WaitForSeconds(10);
        SpawnItem();
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
        //Vector3 spawnPos = currentObjective.GetComponent<Item>().spawnPosition;
        //Quaternion spawnRot = currentObjective.GetComponent<Item>().spawnRotation;
        //print(spawnPos);
        //Instantiate(currentObjective, spawnPos, spawnRot);
        //print(currentObjective.transform.position);

        currentObjective.SetActive(true);
    }
}
