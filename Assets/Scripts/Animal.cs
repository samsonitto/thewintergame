using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    private GameObject player;

    public float health;
    private bool dead;

    public int amountOfItems;
    public GameObject[] item;

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
        player = GameObject.FindWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        currentTimer = timer;
        currentIdleTimer = idleTimer;

        print(anim);
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

        if(health <= 0)
        {
            Die();
        }
    }

    IEnumerator switchIdle()
    {
        idle = true;
        yield return new WaitForSeconds(3);
        currentIdleTimer = 0;
        idle = false;
    }

    public void DropItems()
    {
        for(int i = 0; i < amountOfItems; i++)
        {
            GameObject droppedItem = Instantiate(item[i], transform.position, Quaternion.identity);
            break;
        }
    }

    public void Die()
    {
        DropItems();
        Destroy(this.gameObject);
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
