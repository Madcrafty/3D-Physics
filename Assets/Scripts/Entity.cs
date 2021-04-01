using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header(header: "Hit Points")]
    public int maxHealth = 100;

    [Header(header: "Weak Points")]
    public string weakpointName;
    public float weakpointMod = 1;

    [Header(header: "Knockback Boost (for entities with ragdols)")]
    public float knockbackBoost = 1;

    protected float hp;
    protected Ragdoll ragdoll;
    protected GameManager gm;

    protected virtual void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ragdoll = GetComponent<Ragdoll>();
        foreach (Rigidbody rb in ragdoll.rigidbodies)
        {
            rb.gameObject.AddComponent<HitDetector>().hit.AddListener(TakeDamage);
            rb.gameObject.AddComponent<AddForceHit>().knockbackBoost = knockbackBoost;
        }
        hp = maxHealth;
        RagdollState(false);
    }
    public virtual void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }
    public virtual void TakeDamage(float damage, float knockback, string nameOfHitPart, Vector3 position)
    {
        if (nameOfHitPart == weakpointName)
        {
            hp -= damage * weakpointMod;
        }
        else
        {
            hp -= damage;
        }
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
        ragdoll.RagdollOn = false;
    }
    virtual public void SetActive(bool toggle)
    {
        enabled = toggle;
        RagdollState(!toggle);
    }
    virtual public void RagdollState(bool toggle)
    {

    }
}
