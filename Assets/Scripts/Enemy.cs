using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : Entity
{
    [Header(header: "NavAgent Settings")]
    public Transform target;
    public float speed = 1;
    [Tooltip("path updates performed per second")]
    public float updateRate = 10;

    [Header(header: "Points When Killed")]
    public int pointsWhenKilled = 10;

    [Header(header: "Attack Settings")]
    public int damage = 10;
    public float attackRange = 2;
    public float startupTime = 0.3f;
    public float endingLag = 0.4f;

    [Header(header: "HUD Settings")]
    [Tooltip("The seconds the HUD is active for, when this is hit")]
    public float activeTimeOnHit = 3;

    [Header(header: "Debug")]
    public float distToTarget;
    public bool wandering;

    private Canvas HUD = null;
    private Slider healthBar;
    private NavMeshAgent ai;
    private Animator animator;
    private bool atkInProgress = false;
    private NavMeshPath path;
    private float navUpdateTimer;
    private float hudTimer;

    // Start is called before the first frame update
    protected override void Start()
    {
        path = new NavMeshPath();
        base.Start();
        HUD = transform.GetChild(2).GetComponent<Canvas>();
        HUD.enabled = false;
        animator = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();
        ai.speed = speed;
        healthBar = transform.GetChild(2).GetChild(0).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        navUpdateTimer += Time.deltaTime;
        if (hudTimer > 0)
        {
            hudTimer -= Time.deltaTime;
            if (hudTimer <= 0)
            {
                HUD.enabled = false;
            }
        }
        
        if (navUpdateTimer > 1/updateRate)
        {
            navUpdateTimer = 0;
            ai.CalculatePath(target.position, path);
            if (path.status != NavMeshPathStatus.PathPartial)
            {
                ai.SetDestination(target.position);
                wandering = false;
            }
            else
            {
                ai.SetDestination(RandomNavSphere(transform.position, 10, -1));
                wandering = true;
            }
        }
        animator.SetFloat("Forwards", speed * Time.fixedDeltaTime);
        distToTarget = Vector3.Magnitude(target.position - transform.position);
        if (distToTarget <= attackRange && target.GetComponent<HitDetector>() != null && !atkInProgress)
        {
            StartCoroutine(Attack());
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
    protected override void Die()
    {
        if (!ragdoll.RagdollOn)
        {
            gm.AddScore(pointsWhenKilled);
        }
        base.Die();

    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (!ragdoll.RagdollOn)
        {
            hudTimer = activeTimeOnHit;
            HUD.enabled = true;
            healthBar.value = hp / (float)maxHealth;
        }
    }
    public override void Respawn()
    {
        base.Respawn();
        healthBar.value = hp / (float)maxHealth;
    }
    public override void SetActive(bool toggle)
    {
        base.SetActive(toggle);
        animator.enabled = toggle;
        ai.enabled = toggle;
        if (!toggle)
        {
            HUD.enabled = toggle;
        }   
    }
    public override void RagdollState(bool toggle)
    {
        foreach (Rigidbody r in ragdoll.rigidbodies)
        {
            r.isKinematic = !toggle;
        }
    }
    IEnumerator Attack()
    {
        atkInProgress = true;
        yield return new WaitForSeconds(startupTime);
        if (Vector3.Magnitude(target.position - transform.position) <= attackRange)
        {
            target.GetComponent<HitDetector>().Hit(damage);
        }
        yield return new WaitForSeconds(endingLag);
        atkInProgress = false;
    }
}
