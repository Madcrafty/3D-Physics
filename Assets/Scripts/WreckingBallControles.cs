using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBallControles : MonoBehaviour
{
    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //connectedJoint = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    public void ToggleChain(bool toggle)
    {
        rb.isKinematic = !toggle;
    }
}
