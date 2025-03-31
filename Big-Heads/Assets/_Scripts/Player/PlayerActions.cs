using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private InputHandler inputHandler;
    
    public bool headAttached = true;
    [Space(10)]
    [SerializeField] private Transform head;
    [SerializeField] private new_Head headScript;
    [SerializeField] private Transform body;
    [Header("Throw")]
    [SerializeField] private float throwMult;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        headScript = head.GetComponent<new_Head>();
    }

    public void Teleport()
    {
        if (!headAttached)
        {
            body.position = head.position;
            head.position = body.position + Vector3.up;
            
            headScript.AttachHead(true);
            headAttached = true;
        }
    }

    public void Throw()
    {
        if (headAttached)
        {
            Vector2 force = inputHandler.aim * throwMult;
            head.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            
            headScript.AttachHead(false);
            headAttached = false;
        }
    }
}
