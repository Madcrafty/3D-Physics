using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : Entity
{
    [Header(header: "Target")]
    public Transform target;

    [Header(header: "Move Speed")]
    public float speed = 1;

    [Header(header: "Points When Killed")]
    public int pointsWhenKilled = 10;

    [Header(header: "Attack Settings")]
    public int damage = 10;
    public float attackRange = 2;
    public float startupTime = 0.3f;
    public float endingLag = 0.4f;

    [Header(header: "Debug")]
    public float distToTarget;

    private Canvas HUD = null;
    private Slider healthBar;
    private NavMeshAgent ai;
    private Animator animator;
    private bool atkInProgress = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        HUD = transform.GetChild(2).GetComponent<Canvas>();
        animator = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();
        ai.speed = speed;
        healthBar = transform.GetChild(2).GetChild(0).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        ai.SetDestination(target.position);
        animator.SetFloat("Forwards", speed * Time.fixedDeltaTime);
        distToTarget = Vector3.Magnitude(target.position - transform.position);
        if (distToTarget <= attackRange && target.GetComponent<HitDetector>() != null && !atkInProgress)
        {
            StartCoroutine(Attack());
        }
    }
    protected override void Die()
    {
        base.Die();
        gm.AddScore(pointsWhenKilled);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthBar.value = hp / (float)maxHealth;
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
        HUD.enabled = toggle;
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
