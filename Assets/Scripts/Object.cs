using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Object : MonoBehaviour
{
    public Material awakeMat = null;
    public Material asleepMat = null;

    private Rigidbody rb = null;

    private bool wasAsleep = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    //private void Update()
    //{
    //    if (this.name == "Sphere" && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        rb.AddForce(transform.forward * 100, ForceMode.VelocityChange);
    //    }
    //}
    private void FixedUpdate()
    {
        if (rb.IsSleeping() && !wasAsleep && asleepMat != null)
        {
            wasAsleep = true;
            GetComponent<MeshRenderer>().material = asleepMat;
        }
        if (!rb.IsSleeping() && wasAsleep && awakeMat != null)
        {
            wasAsleep = false;
            GetComponent<MeshRenderer>().material = awakeMat;
        }
    }
}
