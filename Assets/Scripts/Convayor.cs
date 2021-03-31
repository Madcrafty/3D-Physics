using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convayor : MonoBehaviour
{
    public float speed;
    public float visualSpeedScalar;

    private Vector3 direction;
    private float currentScroll;

    private void OnCollisionStay(Collision otherThing)
    {
        // Get the direction of the conveyor belt 
        // (transform.forward is a built in Vector3 
        // which is used to get the forward facing direction)
        // * Remember Vector3's can used for position AND direction AND rotation
        direction = transform.forward;
        direction = direction * speed;

        // Add a WORLD force to the other objects
        // Ignore the mass of the other objects so they all go the same speed (ForceMode.Acceleration)
        otherThing.rigidbody.AddForce(direction, ForceMode.Acceleration);
        //otherThing.rigidbody.MovePosition(otherThing.transform.position + direction);
    }
}
