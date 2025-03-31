using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),
    typeof(CircleCollider2D),
    typeof(SpringJoint2D))]
public class new_Head : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CircleCollider2D _col;
    private SpringJoint2D _joint;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CircleCollider2D>();
        _joint = GetComponent<SpringJoint2D>();
    }

    public void AttachHead(bool isAttached)
    {
        _joint.enabled = isAttached;
        if (isAttached) _joint.distance = 0;
    }
}
