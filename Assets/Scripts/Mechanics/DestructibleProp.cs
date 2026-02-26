using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleProp : MonoBehaviour, IDamageable
{
    [SerializeField] private float force = 10f;

    public void TakeDamage (int damage)
    {
        GetComponent<Rigidbody>().AddForce(-transform.forward * force, ForceMode.VelocityChange);
    }
}
