using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform body; 
    public Rigidbody2D _rb;
    private InputHandler _inputHandler;
    private StatModifiers _statModifiers;
    
    [Header("Movement")]
    [SerializeField] private float speedBase;
    [SerializeField] private float acceleration;
    [SerializeField] private float jumpForce;
    private Vector2 desiredVelocity;

    [Header("Ground Check")] 
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float rayDistance;

    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        _statModifiers = GetComponent<StatModifiers>();
        _rb = body.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // _rb.linearVelocity = new Vector2(_inputHandler.move.x * speedBase * _statModifiers.SpeedMult, _rb.linearVelocityY);
        desiredVelocity = _inputHandler.move.normalized * (speedBase * _statModifiers.SpeedMult);
        
        GroundedCheck();
    }

    private void FixedUpdate() {
        Vector2 currentVelocity = _rb.linearVelocity;
        Vector2 velocityChange = desiredVelocity - currentVelocity;

        float xVelocity = Mathf.Lerp(currentVelocity.x, desiredVelocity.x, Time.fixedDeltaTime * acceleration);
        Vector2 newVelocity = new Vector2(xVelocity, _rb.linearVelocityY);
        _rb.linearVelocity = newVelocity;
    }

    private void GroundedCheck()
    {
        Vector2 botLeft = new Vector2(body.position.x - offset.x, body.position.y - offset.y);
        Vector2 botRight = new Vector2(body.position.x + offset.x, body.position.y - offset.y);

        Debug.DrawRay(botLeft, Vector2.down * rayDistance, Color.magenta);
        Debug.DrawRay(botRight, Vector2.down * rayDistance, Color.magenta);

        bool hitLeft = Physics2D.Raycast(botLeft, Vector2.down, rayDistance, layerMask);
        bool hitRight = Physics2D.Raycast(botRight, Vector2.down, rayDistance, layerMask);

        isGrounded = hitLeft || hitRight;
    }

    public void Jump()
    {
        if (isGrounded) _rb.AddForce(Vector2.up * jumpForce * _statModifiers.JumpMult, ForceMode2D.Impulse);
    }
    
}