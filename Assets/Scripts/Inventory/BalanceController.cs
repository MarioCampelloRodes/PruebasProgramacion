using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceController : MonoBehaviour
{
    public float maxSpeed = 6f;
    public float speedMult = 3f;

    private Rigidbody rb;
    private Vector3 accel;

    void Start()
    {
        TryGetComponent(out rb);
    }

    void Update()
    {
        //accel += new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * Time.deltaTime * speedMult;
        //accel.x = Mathf.Clamp(accel.x, -maxSpeed / 2, maxSpeed / 2);
        //accel.z = Mathf.Clamp(accel.z, -maxSpeed / 2, maxSpeed / 2);
        //return;
        accel = Input.acceleration;
        accel = Quaternion.Euler(90, 0, 0) * accel;
        accel *= speedMult;
    }

    private void FixedUpdate()
    {
        rb.AddForce(accel);
        LimitVelocity();
    }

    void LimitVelocity()
    {
        Vector3 _vel = rb.velocity;
        _vel.x = Mathf.Clamp(_vel.x, -maxSpeed, maxSpeed);
        _vel.z = Mathf.Clamp(_vel.z, -maxSpeed, maxSpeed);
        rb.velocity = _vel;
    }
}
