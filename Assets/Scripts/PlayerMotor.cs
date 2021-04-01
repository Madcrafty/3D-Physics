using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : Entity
{
    //public float speed = 3.0f;
    public float speed = 1.0f;
    public float jumpForce = 10.0f;
    public float cyoteTime = 0.0f;
    public float lookSpeed = 2.0f;

    private Rigidbody rb;
    private Collider col;
    private Camera cam;
    private Gun gun;
    private float lookXLimit = 90.0f;

    
    private bool grounded;
    private float elapsedCyoteTime = 0;
    private Ray ray;

    private float rotationX;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = transform.GetChild(1).GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        cam = transform.GetChild(0).GetComponent<Camera>();
        gun = transform.GetChild(0).GetChild(0).GetComponent<Gun>();
        Cursor.lockState = CursorLockMode.Locked;
        ray = new Ray(col.bounds.center, Vector3.down);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = rb.position;
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        rb.AddForce(transform.forward * Input.GetAxis("Vertical") * speed, ForceMode.Impulse);
        rb.AddForce(transform.right * Input.GetAxis("Horizontal") * speed, ForceMode.Impulse);

        if (grounded && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * speed, ForceMode.Impulse);
        }
    }
    private void FixedUpdate()
    {
        CheckGroundedState();
    }
    void CheckGroundedState()
    {
        RaycastHit hit;
        ray.origin = col.bounds.center;
        if (col.GetType() == typeof(CapsuleCollider) || col.GetType() == typeof(SphereCollider))
        {
            Physics.SphereCast(ray, col.bounds.extents.x, out hit, col.bounds.extents.y + 0.1f);
        }
        else
        {
            Physics.Raycast(ray, out hit, col.bounds.extents.y + 0.1f);
        }
        
        if (hit.transform == null)
        {
            elapsedCyoteTime += Time.fixedDeltaTime;
            if (elapsedCyoteTime >= cyoteTime)
            {
                grounded = false;
            }
        }
        else if (!grounded || elapsedCyoteTime > 0)
        {
            grounded = true;
            elapsedCyoteTime = 0;
        }

    }
    public override void SetActive(bool toggle)
    {
        base.SetActive(toggle);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public override void Respawn()
    {
        base.Respawn();
    }
}
