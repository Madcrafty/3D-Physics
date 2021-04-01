using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactDamageObject : MonoBehaviour
{
    public float knockbackBoost = 1;
    public Rigidbody rb;
    public List<string> dontHit;
    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!OnDontHitList(collision.transform.name))
        {
            if (collision.transform.GetComponent<HitDetector>() != null)
            {
                //collision.transform.GetComponent<HitDetector>().hit.Invoke(Damage(), knockbackBoost, collision.transform.name, collision.transform.position);
                collision.transform.GetComponent<HitDetector>().hit.Invoke(Damage(), Damage() * knockbackBoost, collision.transform.name, collision.transform.position);
            }
            else if (collision.transform.GetComponent<Entity>() != null)
            {
                collision.transform.GetComponent<Entity>().TakeDamage(Damage());
            }
        }
    }
    float Damage()
    {
        return Vector3.Magnitude(rb.velocity) * rb.mass;
    }
    bool OnDontHitList(string a_name)
    {
        foreach (string name in dontHit)
        {
            if (name == a_name)
            {
                return true;
            }
        }
        return false;
    }
}
