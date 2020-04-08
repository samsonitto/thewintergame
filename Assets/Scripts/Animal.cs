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

    public float damageGiven;
    private float playerHealth;

    void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        currentTimer = timer;
        currentIdleTimer = idleTimer;
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
        currentTimer += Time.deltaTime;
        currentIdleTimer += Time.deltaTime;

        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsWalking", false);
            agent.speed = runningSpeed;

            if(distance <= interactionRadius)
            {
                FaceTarget();
                animator.SetTrigger("IsHitting");
                animator.SetBool("IsRunning", false);
                agent.speed = 0f;
                FirstPersonAIO.health -= damageGiven;
            }
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsWalking", true);
            agent.speed = walkingSpeed;
        }


        if(currentTimer >= timer/* && !idle*/)
        {
            Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
            agent.SetDestination(newPosition);
            currentTimer = 0;
        }

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

    //IEnumerator switchIdle()
    //{
    //    idle = true;
    //    yield return new WaitForSeconds(3);
    //    currentIdleTimer = 0;
    //    idle = false;
    //}
    //IEnumerator hitting()
    //{
    //    playerHealth -= damageGiven;
    //    //anim.CrossFade("hit");
    //    animator.SetTrigger("IsHitting");
    //    yield return new WaitForSeconds(2);
    //}

    public void DropItems()
    {
        for(int i = 0; i < item.Length; i++)
        {
            GameObject droppedItem = Instantiate(item[i], transform.position, Quaternion.identity);
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
