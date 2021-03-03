using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : Entity
{
    //public int maxHealth = 100;
    //public string weakpointName;
    //public float weakpointMod = 1;
    public Transform target;
    //public float speed = 1;

    //private float hp;
    private Canvas HUD = null;
    private Slider healthBar;
    //private Ragdoll ragdoll;
    private NavMeshAgent ai;
    private Animator animator;

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
}
