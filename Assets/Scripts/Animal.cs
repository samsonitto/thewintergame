using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    public float radius;
    public float timer;

    private Transform target;
    private NavMeshAgent agent;
    private float currentTimer;

    private bool idle;
    public float idleTimer;
    private float currentIdleTimer;

    private Animation anim;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        currentTimer = timer;
        currentIdleTimer = idleTimer;
    }

    void Update()
    {
        currentTimer += Time.deltaTime;
        currentIdleTimer += Time.deltaTime;

        if(currentIdleTimer >= idleTimer)
        {
            StartCoroutine("switchIdle");
        }

        if(currentTimer >= timer && !idle)
        {
            Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
            agent.SetDestination(newPosition);
            currentTimer = 0;
        }

        if (idle)
            anim.CrossFade("idle");
        else
            anim.CrossFade("walk");
    }

    IEnumerator switchIdle()
    {
        idle = true;
        yield return new WaitForSeconds(3);
        currentIdleTimer = 0;
        idle = false;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        return navHit.position;
    }

}
