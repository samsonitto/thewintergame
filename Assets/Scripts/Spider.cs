using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    private GameObject player;

    public float health;
    private bool dead;

    public int amountOfItems;
    public GameObject[] item;

    public float radius;
    public float lookRadius;
    public float interactionRadius;
    public float timer;

    private Transform target;
    private NavMeshAgent agent;
    private float currentTimer;

    private Animator animator;

    public float walkingSpeed;
    public float runningSpeed;

    public float damageGiven;
    private float playerHealth;


    void OnEnable()
    {
        player = GameObject.FindWithTag("Player");

        currentTimer = timer;
    }

    void Start()
    {
        target = PlayerManager.instance.player.transform;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsWalking", true);
    }

    void Update()
    {
        //playerHealth = FirstPersonAIO.health;
        currentTimer += Time.deltaTime;

        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            agent.speed = runningSpeed;

            if(distance <= interactionRadius)
            {
                FaceTarget();
                animator.SetBool("IsHitting", true);
                FirstPersonAIO.health -= damageGiven;
            }
            else
            {
                animator.SetBool("IsHitting", false);
            }
        }
        else
        {
            agent.speed = walkingSpeed;
        }

        if(currentTimer >= timer)
        {
            Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
            agent.SetDestination(newPosition);
            currentTimer = 0;
        }

        if(health <= 0)
        {
            Die();
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);

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
        animator.SetTrigger("IsDead");
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
