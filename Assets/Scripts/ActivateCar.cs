using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCar : MonoBehaviour
{
    HingeJoint[] wheels;
    private void Start()
    {
        wheels = GetComponentsInParent<HingeJoint>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            ActiveCar(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            ActiveCar(false);
        }
    }
    void ActiveCar(bool toggle)
    {
        foreach (HingeJoint wheel in wheels)
        {
            wheel.useMotor = toggle;
        }
    }
}
