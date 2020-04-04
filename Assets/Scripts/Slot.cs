using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private bool hovered;
    public bool empty;

    public GameObject item;
    public Texture itemIcon;

    private GameObject player;

    void Start()
    {
        hovered = false;
        player = GameObject.FindWithTag("Player");
        
    }

    void Update()
    {
        if (item)
        {
            empty = false;
            itemIcon = item.GetComponent<Item>().icon;
            this.GetComponent<RawImage>().texture = itemIcon;
        }
        else
        {
            empty = true;
            itemIcon = null;
            this.GetComponent<RawImage>().texture = null;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item)
        {
            Item thisItem = item.GetComponent<Item>();

            // checking for item type
            if(thisItem.type == "water")
            {
                player.GetComponent<FirstPersonAIO>().Drink(thisItem.decreaseRate);
                Destroy(item);
            }
            else if (thisItem.type == "meat")
            {
                player.GetComponent<FirstPersonAIO>().Eat(thisItem.decreaseRate);
                Destroy(item);
            }
            else if (thisItem.type == "apple")
            {
                player.GetComponent<FirstPersonAIO>().Eat(thisItem.decreaseRate);
                player.GetComponent<FirstPersonAIO>().Drink(thisItem.decreaseRate * 0.2f);
                Destroy(item);
            }
        }
    }


}
