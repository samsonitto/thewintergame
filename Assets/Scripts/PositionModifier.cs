using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionModifier : MonoBehaviour
{
    public bool grounded;
    private Vector3 posCur;
    private Quaternion rotCur;
    private LayerMask layerMask;
    public float distanceFromTheGround;

    void OnEnable()
    {
        layerMask = LayerMask.GetMask("Ground");
    }
    void Start()
    {
        
    }

    void Update()
    {
        CheckTerrainAngle();
    }

    public void CheckTerrainAngle()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        float change;
        if (Physics.Raycast(ray, out hit, 1.5f, layerMask))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);
            change = hit.distance - distanceFromTheGround;
            rotCur = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            posCur = new Vector3(transform.position.x, hit.point.y - change, transform.position.z);
            //change = hit.distance - 

            grounded = true;

        }
        else
        {
            grounded = false;
        }


        if (grounded)
        {
            transform.position = Vector3.Lerp(transform.position, posCur, Time.deltaTime * 2);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotCur, Time.deltaTime * 2);
        }
        //else
        //{
        //    transform.position = Vector3.Lerp(transform.position, transform.position - Vector3.up * 1f, Time.deltaTime * 5);

        //    rotCur.eulerAngles = Vector3.zero;
        //    transform.rotation = Quaternion.Lerp(transform.rotation, rotCur, Time.deltaTime);

        //}
    }
}
