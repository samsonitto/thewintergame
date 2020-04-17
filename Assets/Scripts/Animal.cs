using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    private GameObject player;

    public float health;
    public float maxHealth;
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
    public float timeBetweenAttacks;
    private float attackTimer;
    public bool playerInRange;
    public float deathTime;

    void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        currentTimer = timer;
        currentIdleTimer = idleTimer;
        maxHealth = health;
    }

    void Start()
    {
        StartCoroutine(addHealth());
        target = PlayerManager.instance.player.transform;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Walk(true);
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        currentTimer += Time.deltaTime;
        currentIdleTimer += Time.deltaTime;

        float distance = Vector3.Distance(target.position, transform.position);

        //CheckTerrainAngle();

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            Run(true);
            Walk(false);
            agent.speed = runningSpeed;

            //if(distance <= interactionRadius)
            //{
            //    FaceTarget();
            //    Run(false);
            //    animator.SetBool("IsIdle", true);
            //    Attack();
                
            //    agent.speed = 0f;
            //}

            if(attackTimer >= timeBetweenAttacks && playerInRange)
            {

                Attack();
                
                
            }
        }
        else
        {
            Run(false);
            Walk(true);
            agent.speed = walkingSpeed;
        }


        if(currentTimer >= timer/* && !idle*/)
        {
            Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
            agent.SetDestination(newPosition);
            currentTimer = 0;
        }

        if(health <= maxHealth * 0.3)
        {
            agent.SetDestination(target.position * -1);
            Run(true);
            Walk(false);
            agent.speed = runningSpeed;
        }

        if (health <= 0)
        {
            StartCoroutine(Die());
        }

    }

    IEnumerator addHealth()
    {
        while (true)
        { // loops forever...
            if (health < maxHealth)
            { // if health < 100...
                health += maxHealth * 0.01f; // increase health and wait the specified time
                yield return new WaitForSeconds(1);
            }
            else
            { // if health >= 100, just yield 
                yield return null;
            }
        }
    }

    public void Walk(bool walk)
    {
        animator.SetBool("IsWalking", walk);
    }
    public void Run(bool walk)
    {
        animator.SetBool("IsRunning", walk);
    }

    public void Attack()
    {
        attackTimer = 0f;

        FaceTarget();
        Run(false);
        animator.SetBool("IsIdle", true);
        agent.speed = 0f;
        animator.SetTrigger("IsHitting");
        FirstPersonAIO.health -= damageGiven;
    }

    public void Dead()
    {
        animator.SetTrigger("Dead");
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

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
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

    IEnumerator Die()
    {
        Dead();
        yield return new WaitForSeconds(deathTime);
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
