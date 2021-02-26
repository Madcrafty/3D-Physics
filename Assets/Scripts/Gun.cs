using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float shotCost = 5.0f;
    public float effectTime = 0.1f;
    public float maxCapacity = 100;
    public float rechargeRate = 1;

    private float capacity;
    private float elapsedLaserTime = 0;
    private Vector3 targetPos;
    private Vector3 originPoint;
    private Collider barrel;
    private LineRenderer laser;
    private Slider chargeMeter;
    // Start is called before the first frame update
    void Start()
    {
        capacity = maxCapacity;
        chargeMeter = transform.GetChild(3).GetChild(0).GetComponent<Slider>();
        laser = GetComponent<LineRenderer>();
        barrel = transform.GetChild(2).GetComponent<Collider>();
        originPoint = barrel.bounds.center;
        originPoint.y -= barrel.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        originPoint = barrel.bounds.center;
        originPoint.y -= barrel.bounds.extents.y;
        laser.SetPosition(0, originPoint);
        if (capacity < maxCapacity)
        {
            capacity += rechargeRate;
        }
        if (capacity > maxCapacity)
        {
            capacity = maxCapacity;
        }
        chargeMeter.value = capacity / maxCapacity;
        if (Input.GetMouseButtonDown(0) && capacity >= shotCost)
        {
            Shoot();
            capacity -= shotCost;
        }
        if (laser.enabled)
        {
            elapsedLaserTime += Time.deltaTime;
            if (elapsedLaserTime >= effectTime)
            {
                laser.enabled = false;
            }
        }
        else if (elapsedLaserTime > 0)
        {
            elapsedLaserTime = 0;
        }
    }
    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo, 500);
        if (hitInfo.point == null)
        {
            targetPos = Camera.main.transform.forward * 500;
        }
        else
        {
            targetPos = hitInfo.point;
        }
        laser.SetPosition(1, targetPos);
        laser.enabled = true;
    }
}
