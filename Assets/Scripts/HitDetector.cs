using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitDetector : MonoBehaviour
{
    public UnityEvent<float, float, string, Vector3> hit = new UnityEvent<float, float, string, Vector3>();
}
