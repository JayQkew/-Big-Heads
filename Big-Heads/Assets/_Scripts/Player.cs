using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public Vector2 aim;
    public bool headAttached = true;

    [SerializeField] private Transform head;
    [SerializeField] private Transform body;

    private void Update()
    {
        AttachedHead();
    }

    public void Aim(InputAction.CallbackContext ctx) => aim = ctx.ReadValue<Vector2>().normalized;
    
    public void Teleport(InputAction.CallbackContext ctx)
    { 
        if (ctx.started && !headAttached)
        {
            body.position = head.GetChild(0).position;
            head.GetChild(0).GetComponentInChildren<SoftBody>().MoveSlime(body.position + Vector3.up);

            headAttached = true;
        }
    }

    private void AttachedHead()
    {
        if (headAttached)
        {
            SoftBody softBody =  head.GetChild(0).GetComponentInChildren<SoftBody>();
            foreach (SpringJoint2D joint in softBody.attachJoints)
            {
                joint.enabled = true;
                joint.connectedBody = body.GetChild(1).GetComponent<Rigidbody2D>();
                joint.distance = 0;
            }
        }
        else
        {
            SoftBody softBody =  head.GetChild(0).GetComponentInChildren<SoftBody>();
            foreach (SpringJoint2D joint in softBody.attachJoints)
            {
                joint.enabled = false;
            } }
    }
}
