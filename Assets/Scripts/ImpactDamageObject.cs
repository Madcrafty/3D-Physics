using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactDamageObject : MonoBehaviour
{
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
                //collision.transform.GetComponent<HitDetector>().Hit((int)Damage());
                collision.transform.GetComponent<HitDetector>().hit.Invoke(Damage(collision.transform.GetComponent<Rigidbody>()), collision.transform.name);
            }
            else if (collision.transform.GetComponent<Entity>() != null)
            {
                collision.transform.GetComponent<Entity>().TakeDamage(Damage(collision.transform.GetComponent<Rigidbody>()));
            }
        }
    }
    float Damage(Rigidbody other_body)
    {
        return Mathf.Abs(Vector3.Magnitude(rb.velocity - other_body.velocity) * rb.mass);
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
