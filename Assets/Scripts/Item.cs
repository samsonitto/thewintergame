using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameObject player;
    public Texture icon;

    public string type;
    public float decreaseRate;

    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    public bool pickedUp;
    public bool equipped;
    public bool flashlightEquipped;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
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

    }

    public void Unequip()
    {
        player.GetComponent<FirstPersonAIO>().weaponEquipped = false;
        equipped = false;
        this.gameObject.SetActive(false);
    }
    public void UnequipFlashlight()
    {
        flashlightEquipped = false;
        this.gameObject.SetActive(false);
    }
}
