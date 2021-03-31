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
<<<<<<< HEAD

    // Update is called once per frame
    public void ToggleChain(bool toggle)
    {
        rb.isKinematic = !toggle;
=======
    // Update is called once per frame
    public void ToggleChain()
    {
        rb.isKinematic = !rb.isKinematic;
>>>>>>> 924804c897c2941fcede2015c8672039fa3c1223
    }
}
