using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerProjectile : MonoBehaviour
{
   public ObjectPool<PlayerProjectile> pool;

    private void OnCollisionEnter(Collision collision)
    {
        pool.Release(this);
    }
}
