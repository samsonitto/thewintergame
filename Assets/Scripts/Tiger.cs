using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tiger : MonoBehaviour
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

    private bool idle;
    public float idleTimer;
    private float currentIdleTimer;

    private Animation anim;
    private Animator animator;

    public float walkingSpeed;
    public float runningSpeed;


    void OnEnable()
    {
        player = GameObject.FindWithTag("Player");

        //agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        currentTimer = timer;
        currentIdleTimer = idleTimer;

        print(anim);
    }

    void Start()
    {
        target = PlayerManager.instance.player.transform;

        agent = GetComponent<NavMeshAgent>();
        walkingSpeed = agent.speed;
        runningSpeed = walkingSpeed * 3;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentTimer += Time.deltaTime;
        currentIdleTimer += Time.deltaTime;

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            //anim.CrossFade("run");
            animator.SetBool("IsRunning", true);
            agent.speed = runningSpeed;

            if (distance <= agent.stoppingDistance)
            {
                //Attack the target

                FaceTarget();
            }

            if (distance <= interactionRadius)
            {
                //anim.CrossFade("hit");
                animator.SetBool("IsHitting", true);
                print("hit");
            }
            else
            {
                animator.SetBool("IsHitting", false);
                print(animator.GetBool("IsHitting"));
            }
        }

        if (currentIdleTimer >= idleTimer)
        {
            StartCoroutine("switchIdle");
        }

        if (currentTimer >= timer && !idle)
        {
            Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
            agent.SetDestination(newPosition);
            currentTimer = 0;
        }

        if (idle)
            animator.SetBool("IsIdle", true);
        //anim.CrossFade("idle");
        else
            animator.SetBool("IsWalking", true);
        //anim.CrossFade("walk");

        if (health <= 0)
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

    IEnumerator switchIdle()
    {
        idle = true;
        yield return new WaitForSeconds(3);
        currentIdleTimer = 0;
        idle = false;
    }

    public void DropItems()
    {
        for (int i = 0; i < amountOfItems; i++)
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
