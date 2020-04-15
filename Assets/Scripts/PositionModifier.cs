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
        if (Physics.Raycast(ray, out hit, 1.5f, layerMask))
        {
            print("step1");
            print(hit.transform.name);
            Debug.DrawLine(transform.position, hit.point, Color.green);
            rotCur = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            posCur = new Vector3(transform.position.x, hit.point.y, transform.position.z);

            grounded = true;

        }
        else
        {
            grounded = false;
        }


        if (grounded)
        {
            transform.position = Vector3.Lerp(transform.position, posCur, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotCur, Time.deltaTime * 5);
        }
        //else
        //{
        //    transform.position = Vector3.Lerp(transform.position, transform.position - Vector3.up * 1f, Time.deltaTime * 5);

        //    rotCur.eulerAngles = Vector3.zero;
        //    transform.rotation = Quaternion.Lerp(transform.rotation, rotCur, Time.deltaTime);

        //}
    }
}
