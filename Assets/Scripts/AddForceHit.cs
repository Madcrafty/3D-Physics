using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceHit : MonoBehaviour
{
    public Rigidbody rb;
    public float knockbackBoost = 1;
    //private HitDetector hitDetector;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<HitDetector>().hit.AddListener(AddForce);
        //connectedJoint = GetComponentInParent<Rigidbody>();
    }
    public void AddForce(float damage, float knockback, string parthit, Vector3 poshit)
    {
        Vector3 forcedir = Vector3.Normalize(transform.position - poshit);
        rb.AddForceAtPosition(forcedir * knockback * knockbackBoost, poshit, ForceMode.Impulse);
    }
}
