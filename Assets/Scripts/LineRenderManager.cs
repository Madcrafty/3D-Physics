using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LineRenderManager : MonoBehaviour
{
    // It is really anoying how the linerender changes the poivot point of the gun, this is designed to fix that
    private Collider barrel;
    private LineRenderer laser;
    void Awake()
    {
        laser = GetComponent<LineRenderer>();
        barrel = transform.GetChild(2).GetComponent<Collider>();
    }

    void Update()
    {
        Vector3 originPoint = barrel.bounds.center;
        laser.SetPosition(0, originPoint);
        laser.SetPosition(1, originPoint);
    }
}
