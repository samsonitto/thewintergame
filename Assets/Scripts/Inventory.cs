using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject slotHolder;
    public GameObject itemManager;
    public Text itemInfo;
    
    public bool inventoryEnabled;

    private int slots;
    private Transform[] slot;

    private GameObject itemPickedUp;
    private bool itemAdded;
    private Camera fpsCam;
    private int itemLayerMask;
    private GameObject player;

    private RaycastHit hit;
    private bool isLit;

    public void Start()
    {
        // slots being detected
        slots = slotHolder.transform.childCount;
        slot = new Transform[slots];
        DetectInventorySlots();
        Cursor.visible = false;

        fpsCam = Camera.main;
        itemLayerMask = LayerMask.GetMask("Item");
        player = GameObject.FindWithTag("Player");
        
    }

    public void Update()
    {


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryEnabled = !inventoryEnabled;
            
            
        }

        if (inventoryEnabled)
        {
            Cursor.visible = true;
            player.GetComponent<FirstPersonAIO>().lockAndHideCursor = false;
            player.GetComponent<FirstPersonAIO>().enableCameraMovement = false;
        }
        else
        {
            Cursor.visible = false;
            player.GetComponent<FirstPersonAIO>().lockAndHideCursor = true;
            player.GetComponent<FirstPersonAIO>().enableCameraMovement = true;
        }

        if (inventoryEnabled)
            inventory.GetComponent<Canvas>().enabled = true;
        else
            inventory.GetComponent<Canvas>().enabled = false;



        PickUp();
        
    }

    public void PickUp()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 3f, itemLayerMask))
        {
            
            if (!hit.transform.IsChildOf(player.transform))
            {
                GetComponent<FirstPersonAIO>().ShowInfo(hit.transform.gameObject.name, true);
            }
            

            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject item = hit.transform.gameObject.gameObject;
                itemPickedUp = item;

                if(item.GetComponent<Item>().type != "Campfire")
                    AddItem(item);
            }            

            if (Input.GetKeyDown(KeyCode.G))
            {
                isLit = !isLit;
                GameObject item = hit.transform.gameObject.gameObject;
                item.GetComponentInChildren<Campfire>().TurnOnOff(isLit);
            }
        }
        else
        {
            GetComponent<FirstPersonAIO>().ShowInfo("nothing", false);
        }
        
    }

    public void AddItem(GameObject item)
    {
        for(int i = 0; i < slots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty && itemAdded == false)
            {
                slot[i].GetComponent<Slot>().item = itemPickedUp;
                slot[i].GetComponent<Slot>().itemIcon = itemPickedUp.GetComponent<Item>().icon;

                item.transform.parent = itemManager.transform;
                item.transform.position = itemManager.transform.position;

                item.transform.localPosition = item.GetComponent<Item>().position;
                item.transform.localEulerAngles = item.GetComponent<Item>().rotation;
                item.transform.localScale = item.GetComponent<Item>().scale;

                item.GetComponent<Item>().pickedUp = true; //ehkä joutuu poistaa
                //Destroy(item.GetComponent<Rigidbody>());
                itemAdded = true;
                item.SetActive(false);
            }
        }
        itemAdded = false;
    }

    public void DetectInventorySlots()
    {

        for(int i = 0; i < slots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i);
        }
    }
}
