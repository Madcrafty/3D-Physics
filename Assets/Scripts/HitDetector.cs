using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitDetector : MonoBehaviour
{
    public UnityEvent<float, string> hit = new UnityEvent<float, string>();
}
