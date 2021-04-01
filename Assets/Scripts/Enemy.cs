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
    [Tooltip("Number of times the NavAgent Updates its pathing")]
    public float pathUpdateRate = 2;

    [Header(header: "Points When Killed")]
    public int pointsWhenKilled = 10;

    [Header(header: "Attack Settings")]
    public int damage = 10;
    public float knockback = 0.1f;
    public float attackRange = 2;
    public float startupTime = 0.3f;
    public float endingLag = 0.4f;

    [Header(header: "HUD Settings")]
    [Tooltip("Number of times the NavAgent Updates its pathing")]
    public bool hudActiveOnSpawn = false;
    public float hudActiveTime = 3;

    [Header(header: "Debug")]
    public float distToTarget;

    private Canvas HUD = null;
    private Slider healthBar;
    private NavMeshAgent ai;
    private Animator animator;
    private bool atkInProgress = false;
    private NavMeshPath path;
    private float pathUpdateTimer;
    private float hudDeactivationTimer;

    // Start is called before the first frame update
    protected override void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("Player").transform;
        }
        path = new NavMeshPath();
        base.Start();
        HUD = transform.GetChild(2).GetComponent<Canvas>();
        if (hudActiveOnSpawn)
        {
            hudDeactivationTimer = 0;
        }
        else
        {
            hudDeactivationTimer = hudActiveTime;
            HUD.enabled = false;
        }
        animator = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();
        ai.speed = speed;
        healthBar = transform.GetChild(2).GetChild(0).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        pathUpdateTimer += Time.deltaTime;
        if (pathUpdateTimer >= 1/pathUpdateRate)
        {
            ai.CalculatePath(target.position, path);
            if (path.status != NavMeshPathStatus.PathPartial)
            {
                ai.SetDestination(target.position);
            }
            else
            {
                ai.SetDestination(RandomNavSphere(transform.position, 10, -1));
            }
        }
        if (hudDeactivationTimer < hudActiveTime)
        {
            hudDeactivationTimer += Time.deltaTime;
            if (hudDeactivationTimer >= hudActiveTime)
            {
                HUD.enabled = false;
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
        HUD.enabled = true;
        base.TakeDamage(damage);
        //healthBar.value = hp / maxHealth;
        UpdateHealthBar();
    }
    public override void TakeDamage(float damage, float knockback, string nameOfHitPart, Vector3 position)
    {
        HUD.enabled = true;
        base.TakeDamage(damage, knockback, nameOfHitPart, position);
        //healthBar.value = hp / maxHealth;
        UpdateHealthBar();
    }
    public override void Respawn()
    {

        HUD.enabled = hudActiveOnSpawn;
        base.Respawn();
        //healthBar.value = hp / maxHealth;
        UpdateHealthBar();
    }
    public override void SetActive(bool toggle)
    {
        base.SetActive(toggle);
        animator.enabled = toggle;
        ai.enabled = toggle;
        if (toggle == hudActiveOnSpawn)
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
            //target.GetComponent<HitDetector>().Hit(damage);
            target.GetComponent<HitDetector>().hit.Invoke(damage, knockback, target.name, transform.position);
        }
        yield return new WaitForSeconds(endingLag);
        atkInProgress = false;
    }

    void UpdateHealthBar()
    {
        healthBar.value = hp / maxHealth;
        hudDeactivationTimer = 0;
    }
}
