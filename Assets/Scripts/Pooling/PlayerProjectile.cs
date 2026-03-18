using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerProjectile : MonoBehaviour
{
   public ObjectPool<PlayerProjectile> pool;

    [SerializeField] private Rigidbody rb;

    public void Shoot(Vector3 force)
    {
        rb.AddForce(force, ForceMode.VelocityChange);
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        pool.Release(this);
    }
}
