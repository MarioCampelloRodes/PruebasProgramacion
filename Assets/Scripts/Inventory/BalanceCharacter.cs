using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceCharacter : MonoBehaviour
{
    public Rigidbody ball;
    public Vector3 offset;
    private Animator anim;

    private void Start()
    {
        TryGetComponent(out anim);
    }

    void Update()
    {
        transform.position = ball.transform.position + offset;
        anim.SetFloat("Direction", Mathf.Sign(ball.velocity.z));
    }
}
