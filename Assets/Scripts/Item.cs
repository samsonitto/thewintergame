﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameObject player;
    private GameObject thisItem;
    public Texture icon;

    public string type;
    public float decreaseRate;

    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    public bool pickedUp;
    public bool equipped;
    public bool flashlightEquipped;

    private float pickupRadius = 2f;
    private Camera fpsCam;
    private int itemLayerMask;

    public string partName;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;

    public float endGameRadius;
    public bool itemDropped = false;

    void Start()
    {
        itemLayerMask = LayerMask.GetMask("Item");
        player = GameObject.FindWithTag("Player");
        fpsCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (equipped)
        {
            if (Input.GetKeyDown(KeyCode.E))
                Unequip();
        }

        if (flashlightEquipped)
        {
            if (Input.GetKeyDown(KeyCode.T))
                UnequipFlashlight();
        }

        if (itemDropped)
        {
            StartCoroutine(droppedItemTimer());
        }
        else
        {
            StopCoroutine(droppedItemTimer());
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, endGameRadius);

    }

    public void Unequip()
    {
        player.GetComponent<FirstPersonAIO>().weaponEquipped = false;
        equipped = false;
        this.gameObject.SetActive(false);
    }
    public void Equip()
    {
        player.GetComponent<FirstPersonAIO>().weaponEquipped = true;
        equipped = true;
        this.gameObject.SetActive(true);
    }
    public void UnequipFlashlight()
    {
        flashlightEquipped = false;
        this.gameObject.SetActive(false);
    }

    public IEnumerator droppedItemTimer()
    {
        yield return new WaitForSeconds(30);
        Destroy(this.gameObject);
    }
}
