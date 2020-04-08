using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public float damage = 25f;
    public float range = 200f;

    public Camera fpsCam;
    public int layerMask;

    public string type;
    private Item thisItem;

    private GameObject player;
    private bool equipWeapon;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private AudioSource gunShot;
    void OnEnable()
    {
        fpsCam = Camera.main;
        layerMask = LayerMask.GetMask("Enemy");
        
    }

    void Start()
    {
        thisItem = GetComponent<Item>();
        player = GameObject.FindWithTag("Player");
        gunShot = GetComponent<AudioSource>();
    }
    void Update()
    {
        

        if(Input.GetButtonDown("Fire1") && GetComponent<Item>().equipped)
        {
            print("step0");
            if (!player.GetComponent<Inventory>().inventoryEnabled)
            {
                print("step1");
                Shoot();
            }
                
        }
        
    }


    void Shoot()
    {
        muzzleFlash.Play();
        gunShot.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask))
        {
            print("step2");
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.red, 1f);
            Debug.Log(hit.transform.name);
            Animal animal = hit.transform.GetComponent<Animal>();
            Spider spider = hit.transform.GetComponent<Spider>();

            if (animal != null)
            {
                print("step animal");
                animal.health -= damage;
            }
            else if (spider != null)
            {
                print("step spider");
                spider.health -= damage;
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    void EquipWeapon()
    {
        player.GetComponent<FirstPersonAIO>().weaponEquipped = !player.GetComponent<FirstPersonAIO>().weaponEquipped;

        print(player.GetComponent<FirstPersonAIO>().weaponEquipped);

        if (thisItem.type == "Weapon" && !player.GetComponent<FirstPersonAIO>().weaponEquipped)
        {
            thisItem.equipped = true;
            GetComponent<Slot>().item.SetActive(true);
            //player.GetComponent<FirstPersonAIO>().weaponEquipped = true;
        }
        else
        {
            thisItem.equipped = false;
            GetComponent<Slot>().item.SetActive(false);
            //player.GetComponent<FirstPersonAIO>().weaponEquipped = false;
        }
    }
}
