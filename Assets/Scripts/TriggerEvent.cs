using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent events;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            events.Invoke();
        }
    }
}
