using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int maxHealth = 100;
    public string weakpointName;
    public float weakpointMod = 1;
    public float speed = 1;

    protected float hp;
    protected Ragdoll ragdoll;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
        foreach (Rigidbody rb in ragdoll.rigidbodies)
        {
            rb.gameObject.AddComponent<HitDetector>();
        }
        hp = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void TakeDamage(float damage)
    {
        hp -= damage;
        //healthBar.value = hp / (float)maxHealth;
        if (hp <= 0)
        {
            Die();
        }
    }
    virtual protected void Die()
    {
        ragdoll.RagdollOn = true;
    }
    virtual public void Respawn()
    {
        hp = maxHealth;
        //healthBar.value = hp / (float)maxHealth;
        ragdoll.RagdollOn = false;
    }
    virtual public void SetActive(bool toggle)
    {
        enabled = toggle;
    }
}
