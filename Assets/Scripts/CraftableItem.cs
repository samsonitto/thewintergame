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

    private bool hovered;
    private GameObject player;
    private GameObject itemManager;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        itemManager = GameObject.FindWithTag("ItemManager");
    }

    public void Update()
    {
        if (hovered)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckForRequiredItems();
            }
        }
    }

    public void CheckForRequiredItems()
    {
        int itemsInManager = itemManager.transform.childCount;

        if (itemsInManager > 0)
        {
            int itemsFound = 0;

            for(int i = 0; i < itemsInManager; i++)
            {
                for(int j = 0; j < requiredItems; j++)
                {
                    if (itemManager.transform.GetChild(i).GetComponent<Item>().type == item[j].GetComponent<Item>().type)
                    {
                        itemsFound++;
                        break;
                    }
                }
            }

            if (itemsFound >= requiredItems)
            {
                Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                GameObject spawnedItem = Instantiate(thisItem, pos, Quaternion.identity);
                player.GetComponent<Inventory>().AddItem(spawnedItem);
            }

        }
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
