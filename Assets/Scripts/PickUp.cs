using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Camera fpsCamera;
    private LayerMask layerMask;
    private float pickupTime;
    private Item itemBeingPickedUp;
    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectItemBeingPickedUpFromRay()
    {
        Ray ray = fpsCamera.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, 2f, layerMask))
        {
            var hitItem = hitInfo.collider.GetComponent<Item>();

            if(hitItem == null)
            {
                itemBeingPickedUp = null;
            }
            else if (hitItem != null && hitItem != itemBeingPickedUp)
            {
                itemBeingPickedUp = hitItem;
            }
        }
        else
        {
            itemBeingPickedUp = null;
        }
    }
}
