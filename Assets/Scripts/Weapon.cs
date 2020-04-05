using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 25f;
    public float range = 100f;

    public Camera fpsCam;
    public int layerMask;

    void OnEnable()
    {
        fpsCam = Camera.main;
        layerMask =~ LayerMask.GetMask("Ignore Raycast");
    }
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && GetComponent<Item>().equipped)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
