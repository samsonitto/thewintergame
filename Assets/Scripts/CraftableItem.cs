using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftableItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject thisItem;

    public int requiredItems;
    public GameObject[] item;

    public bool hovered;
    private GameObject player;
    private GameObject itemManager;
    private GameObject craftingManager;
    private GameObject[] itemsUsedForCrafting;
    private Text inventoryInfo;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        itemManager = GameObject.FindWithTag("ItemManager");
        craftingManager = GameObject.FindWithTag("CraftingManager");
        inventoryInfo = player.GetComponent<Inventory>().itemInfo;
    }

    public void Update()
    {
        if (hovered)
        {
            print("step0");
            if (Input.GetMouseButtonDown(0))
            {
                CheckForRequiredItems();
            }

            string items = "";

            for(int i = 0; i< item.Length; i++)
            {
                if(i == item.Length - 1)
                {
                    items += (item[i].transform.name + ".");
                    print("step" + i);
                }

                else
                {
                    items += (item[i].transform.name + ", ");
                    print("step" + i);
                }
                    
            }
            print("items: " + items);
            print(inventoryInfo.text);
            inventoryInfo.text = "To craft " + thisItem.transform.name + " you need these items: " + items;
        }
        else
        {
            //inventoryInfo.text = "";
        }
    }

    public void CheckForRequiredItems()
    {
        int itemsInManager = itemManager.transform.childCount;

        if (itemsInManager > 0)
        {
            int itemsFound = 0;

            itemsUsedForCrafting = new GameObject[requiredItems];

            for(int i = 0; i < itemsInManager; i++)
            {
                for(int j = 0; j < requiredItems; j++)
                {
                    if (itemManager.transform.GetChild(i).GetComponent<Item>().type == item[j].GetComponent<Item>().type)
                    {
                        itemsUsedForCrafting[itemsFound] = itemManager.transform.GetChild(i).gameObject;
                        itemsFound++;
                        break;
                    }
                }

                if (itemsFound == requiredItems)
                    break;
            }

            if (itemsFound >= requiredItems)
            {
                Vector3 playerPos = player.transform.position;
                Vector3 playerDirection = player.transform.forward;
                Quaternion playerRotation = player.transform.rotation;
                float spawnDistance = 3;

                Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

                Instantiate(thisItem, spawnPos, playerRotation);

                for (int i = 0; i < requiredItems; i++)
                {
                    Destroy(itemsUsedForCrafting[i]);
                }
            }

        }
    }

    public void ShowInfo()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }


}
