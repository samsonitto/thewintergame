using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public GameObject pointLight;
    // Start is called before the first frame update
    void Start()
    {
        TurnOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnOff(bool isLit)
    {
        if (isLit)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public void TurnOn()
    {
        this.gameObject.GetComponent<ParticleSystem>().Play();
        pointLight.GetComponent<Light>().enabled = true;
    }

    public void TurnOff()
    {
        this.gameObject.GetComponent<ParticleSystem>().Stop();
        pointLight.GetComponent<Light>().enabled = false;
    }
}
