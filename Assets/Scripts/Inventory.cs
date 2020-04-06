using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject slotHolder;
    public GameObject itemManager;
    
    public bool inventoryEnabled;

    private int slots;
    private Transform[] slot;

    private GameObject itemPickedUp;
    private bool itemAdded;
    private Camera fpsCam;
    private int itemLayerMask;
    private GameObject player;

    private RaycastHit hit;

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
            //Cursor.visible = !Cursor.visible;
            player.GetComponent<FirstPersonAIO>().lockAndHideCursor = !player.GetComponent<FirstPersonAIO>().lockAndHideCursor;
            player.GetComponent<FirstPersonAIO>().enableCameraMovement = !player.GetComponent<FirstPersonAIO>().enableCameraMovement;
            
        }

        if (inventoryEnabled)
            inventory.GetComponent<Canvas>().enabled = true;
        else
            inventory.GetComponent<Canvas>().enabled = false;



        PickUp();
        
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Item")
    //    {
    //        print("Colliding!");
    //        itemPickedUp = other.gameObject;
    //        AddItem(itemPickedUp);
    //    }
    //}

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
                print("F");
                GameObject item = hit.transform.gameObject.gameObject;
                itemPickedUp = item;
                //Debug.Log(item);
                AddItem(item);
            }
        }
        else
        {
            GetComponent<FirstPersonAIO>().ShowInfo("nothing", false);
        }
        
    }

    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Item")
    //        itemAdded = false;
    //}

    public void AddItem(GameObject item)
    {
        print("add");
        for(int i = 0; i < slots; i++)
        {
            print("add" + i);
            if (slot[i].GetComponent<Slot>().empty && itemAdded == false)
            {
                slot[i].GetComponent<Slot>().item = itemPickedUp;
                Debug.Log(itemPickedUp);
                slot[i].GetComponent<Slot>().itemIcon = itemPickedUp.GetComponent<Item>().icon;

                item.transform.parent = itemManager.transform;
                item.transform.position = itemManager.transform.position;

                item.transform.localPosition = item.GetComponent<Item>().position;
                item.transform.localEulerAngles = item.GetComponent<Item>().rotation;
                item.transform.localScale = item.GetComponent<Item>().scale;

                item.GetComponent<Item>().pickedUp = true; //ehkä joutuu poistaa
                Destroy(item.GetComponent<Rigidbody>());
                itemAdded = true;
                print("done");
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
